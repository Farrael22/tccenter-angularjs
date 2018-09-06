using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using balcao.offline.api.Helpers.Business;
using balcao.offline.api.DTO;
using balcao.offline.api.Entity;
using balcao.offline.api.DataAccess.Repository;
using balcao.offline.api.Helpers.Exceptions;

namespace balcao.offline.api.Business
{
    public class ProdutosBusiness : IProdutosBusiness    {

        private readonly IProdutosRepository _produtosRepository;
        private const int TAMANHO_MINIMO_CODIGO_BARRAS = 7;
        private const int TAMANHO_MAXIMO_CODIGO_BARRAS = 13;

        public ProdutosBusiness(IProdutosRepository produtosRepository)
        {
            _produtosRepository = produtosRepository;
        }

        public IEnumerable<ProdutoDTO> Busca(int filial, string texto)
        {
            IEnumerable<ProdutoEntity> listaEntidadeProduto = null;
            var retorno = new List<ProdutoDTO>();

            if (this.IsNumeric(texto))
            {
                #region Pesquisa por CodigoBarra ou CodigoInterno

                long codigo = long.Parse(texto.Substring(0, texto.Length - 1));
                int digito = int.Parse(texto.Last().ToString());

                // Verifica se foi passado um codigo de barras ou apenas o id interno
                if (texto.Length > TAMANHO_MINIMO_CODIGO_BARRAS && texto.Length <= TAMANHO_MAXIMO_CODIGO_BARRAS)
                {
                    var codigoInterno = this.ObterCodigoInternoProdutoPorCodigoBarra(texto);
                    if (codigoInterno > 0 && codigoInterno.ToString().Length > 1)
                    {
                        codigo = long.Parse(codigoInterno.ToString().Substring(0, codigoInterno.ToString().Length - 1));
                        digito = int.Parse(codigoInterno.ToString().Last().ToString());
                    }
                }

                if (codigo > 0)
                {
                    listaEntidadeProduto = _produtosRepository.ListarProdutosPorCodigoInterno(filial, codigo, digito);
                }
                else
                {
                    return retorno;
                }
                    
                #endregion
            }
            else
            {
                #region Pesquisa por Fonema ou Descrição

                string fonema = RetornaFonema(texto);
                string strProduto = texto.Replace("*", "%");

                if (!strProduto.Contains("%"))
                    strProduto += "%";

                if (!string.IsNullOrEmpty(fonema))
                {
                    listaEntidadeProduto = _produtosRepository.ListarProdutosPorFonema(filial, strProduto, fonema);
                }
                else
                {
                    listaEntidadeProduto = _produtosRepository.ListarProdutosPorDescricao(filial, strProduto);
                }

                #endregion
            }

            if (listaEntidadeProduto.Count() > 0)
            {
                DateTime dataAtualizacao = listaEntidadeProduto.FirstOrDefault().DataAtualizacao;
                if (dataAtualizacao.Date < DateTime.Now.AddDays(-1).Date)
                    throw new DataAtualizacaoException(dataAtualizacao.Date.ToString());
            }

            listaEntidadeProduto = listaEntidadeProduto.Where(p => p.Codigo != 0);

            retorno = (List<ProdutoDTO>)Mapper.Map<IEnumerable<ProdutoEntity>, IEnumerable<ProdutoDTO>>(listaEntidadeProduto);

            this.AdicionarCodigosDeBarrasNaLista(retorno);

            this.AdicionarOrdenacaoNaLista(retorno, texto);

            return retorno.OrderBy(d => d.Ordenacao);
        }

        private void AdicionarCodigosDeBarrasNaLista(List<ProdutoDTO> lista)
        {
            if (!(lista != null && lista.Count > 0))
                return;

            var codigos = _produtosRepository.ObterCodigoBarraPorCodigoInternoProduto(lista.Select(q => q.Codigo).ToList());

            lista.ForEach(p => p.CodigosBarra = codigos.Where(d => d.CodProduto == p.Codigo).OrderBy(o => o.Principal).Select(d => d.Codigo).ToList());
        }

        private void AdicionarOrdenacaoNaLista(List<ProdutoDTO> lista, string textoPesquisa = null)
        {
            if (!(lista != null && lista.Count > 0))
                return;

            List<int> produtos = lista.Select(q => q.Codigo).ToList();
            var ordenacaoDaPesquisa = _produtosRepository.ObterOrdenacaoProdutos(produtos);

            if (!(ordenacaoDaPesquisa != null && ordenacaoDaPesquisa.Count() > 0))
                return;

            foreach (var item in lista)
            {
                var ordernacao = ordenacaoDaPesquisa.FirstOrDefault(q => q.CD_PRODUTO == item.Codigo);
                if (ordernacao == null)
                    continue;

                item.PrioridadeVenda = (ordernacao.CARAC != 999) ? ordernacao.CARAC : 0;
                item.Ordenacao = ordernacao.NumLinha;

                //Implementação adaptada do CosmosBalcao
                if (textoPesquisa != null && !this.IsNumeric(textoPesquisa))
                    if (ValidaDescricaoContemTextoPesquisado(item.DescricaoResumida.ToUpper(), textoPesquisa.ToUpper()))
                        item.Ordenacao -= lista.Count();
            }
        }

        private string RetornaFonema(string texto)
        {
            try
            {
                string fonema = "";

                if (texto.Length > 3)
                {
                    fonema = FonemaHelper.montaFraseFiltroInteligente(texto);
                    if (fonema.Length > 3)
                        fonema = fonema.Substring(1, fonema.Length - 2);
                }
                return fonema;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsNumeric(string texto)
        {
            Int64 numero;
            return (Int64.TryParse(texto, out numero));
        }

        private long ObterCodigoInternoProdutoPorCodigoBarra(string codigoBarra)
        {
            if (String.IsNullOrEmpty(codigoBarra))
                return 0;

            ProdutoEntity produto = _produtosRepository.ObterCodigoInternoProdutoPorCodigoBarra(codigoBarra.PadLeft(14, '0')).FirstOrDefault();
            return produto != null ? produto.CodigoInterno : 0;
        }

        private bool ValidaDescricaoContemTextoPesquisado(string descricao, string texto)
        {
            if (texto.IndexOf(" ") < 0)
            {
                int indiceSpaco = descricao.IndexOf(" ");
                indiceSpaco = (indiceSpaco > 0 ? indiceSpaco : descricao.Length);
                //return (texto.Equals(descricao.Substring(0, indiceSpaco)));
                return (descricao.Substring(0, indiceSpaco).Contains(texto));
            }
            else
            {
                string[] vetDescricao = descricao.Split(' ');
                string[] vetTexto = texto.Split(' ');
                if (vetDescricao.Count() == 0 || vetTexto.Count() == 0)
                    return false;
                if (vetDescricao.Count() < 2 || vetTexto.Count() < 2)
                    return false;

                return (vetDescricao[0].Equals(vetTexto[0]) && vetDescricao[1].Contains(vetTexto[1]));
            }
        }

        public IEnumerable<ProdutoDTO> Equivalentes(int filial, int codigoProduto)
        {
            codigoProduto = int.Parse(codigoProduto.ToString().Remove(codigoProduto = codigoProduto.ToString().Length - 1));

            IEnumerable<int> codigosEquivalentes = _produtosRepository.ListarProdutosEquivalentes(filial, codigoProduto);

            IEnumerable<ProdutoEntity> produtosEquivalentes = _produtosRepository.ObterProdutosEquivalentes(filial, codigosEquivalentes.ToList());

            if (produtosEquivalentes.Count() > 0)
            {
                DateTime dataAtualizacao = produtosEquivalentes.FirstOrDefault().DataAtualizacao;
                if (dataAtualizacao.Date < DateTime.Now.AddDays(-1).Date)
                    throw new DataAtualizacaoException(dataAtualizacao.Date.ToString());
            }

            produtosEquivalentes = produtosEquivalentes.Where(p => p.Codigo != 0);

            var listaProdutosRetorno = (List<ProdutoDTO>)Mapper.Map<IEnumerable<ProdutoEntity>, IEnumerable<ProdutoDTO>>(produtosEquivalentes);

            this.AdicionarCodigosDeBarrasNaLista(listaProdutosRetorno);

            this.AdicionarOrdenacaoNaLista(listaProdutosRetorno);

            return listaProdutosRetorno.OrderBy(d => d.Ordenacao);
        }

    }
}