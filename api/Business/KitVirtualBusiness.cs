using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using balcao.offline.api.Helpers.Business;
using balcao.offline.api.DataAccess.Repository;
using balcao.offline.api.DTO;
using balcao.offline.api.Entity;
using balcao.offline.api.Domain.Enum;
using balcao.offline.api.Helpers.Exceptions;

namespace balcao.offline.api.Business
{
    public class KitVirtualBusiness : IKitVirtualBusiness
    {
        private readonly IKitVirtualRepository _kitVirtualRepository;
        public KitVirtualBusiness(IKitVirtualRepository kitVirtualRepository)
        {
            _kitVirtualRepository = kitVirtualRepository;
        }

        public IEnumerable<KitVirtualDTO.KitVirtualRetorno> ObterKitsVirtuais(List<KitVirtualDTO.Produto> listaProdutos)
        {
            IEnumerable<KitVirtualEntity> kitVirtualEntity  = _kitVirtualRepository.ListarKitsVirtuais(ListaCodigosProdutos(listaProdutos));

            if (kitVirtualEntity.Count() > 0)
            {
                DateTime dataAtualizacao = kitVirtualEntity.FirstOrDefault().DataAtualizacao;
                if (dataAtualizacao.Date < DateTime.Now.AddDays(-1).Date)
                    throw new DataAtualizacaoException(dataAtualizacao.Date.ToString());
            }

            var retornopesquisa = kitVirtualEntity.Where(k => k.CodigoKit != null).ToList();

            var listaKitsPossiveis = PrepararKits(retornopesquisa);
            var listaKitsFormados = ProcessarKitsParaProdutos(listaKitsPossiveis, listaProdutos);

            return listaKitsFormados;
        }

        private List<long> ListaCodigosProdutos(List<KitVirtualDTO.Produto> listaProdutos)
        {
            List<long> listaCodigos = new List<long>();

            for (int i = 0; i < listaProdutos.Count; i++)
            {
                listaCodigos.Add(listaProdutos[i].CodigoProduto);
            }

            return listaCodigos;
        }

        private List<KitVirtualDTO.KitVirtualEstruturado> PrepararKits(List<KitVirtualEntity> retornopesquisa)
        {
            var listaKitsPossiveis = new List<KitVirtualDTO.KitVirtualEstruturado>();

            string auxCodigoKitAnterior = "";

            foreach (var item in retornopesquisa)
            {
                if (item.CodigoKit != auxCodigoKitAnterior)
                {
                    listaKitsPossiveis.Add(new KitVirtualDTO.KitVirtualEstruturado
                    {
                        CodigoKit = item.CodigoKit,
                        InicioVigencia = item.InicioVigencia,
                        FimVigencia = item.FimVigencia,
                        TipoRegra = item.TipoRegra,
                        TipoAplicacao = item.TipoAplicacao,
                        TituloKit = item.TituloKit,
                        DescricaoKit = item.DescricaoKit,
                        ValorDesconto = item.ValorDesconto,
                        PercentualDesconto = item.PercentualDesconto,
                        MaxPorCupom = item.MaxPorCupom,
                        QtdMinProdutoA = item.QtdMinProdutoA,
                        QtdMinProdutoB = item.QtdMinProdutoB,
                        ValorIndenizacao = item.ValorIndenizacao,

                        ListaProdutosA = new List<KitVirtualDTO.ProdutoKitVirtual>(),
                        ListaProdutosB = new List<KitVirtualDTO.ProdutoKitVirtual>(),
                    });

                    auxCodigoKitAnterior = item.CodigoKit;
                }
                var produto = new KitVirtualDTO.ProdutoKitVirtual
                {
                    CodigoProduto = item.CodigoProduto,
                    PrecoPraticadoAraujo = item.PrecoPraticado,
                    CustoNegociado = item.CustoNegociado,
                    IndicadorProdutoIdenizado = item.IndicadorProdutoIdenizado
                };

                if (item.ListaProduto == ((char)TipoListaProdutoKitEnum.ListaA).ToString())
                {
                    listaKitsPossiveis[listaKitsPossiveis.Count - 1].ListaProdutosA.Add(produto);
                }
                else if (item.ListaProduto == ((char)TipoListaProdutoKitEnum.ListaB).ToString())
                {
                    listaKitsPossiveis[listaKitsPossiveis.Count - 1].ListaProdutosB.Add(produto);
                }
            }
            return listaKitsPossiveis;
        }

        private List<KitVirtualDTO.KitVirtualRetorno> ProcessarKitsParaProdutos(List<KitVirtualDTO.KitVirtualEstruturado> listaKitsPossiveis, List<KitVirtualDTO.Produto> listaProdutos)
        {
            var kitsFormados = new List<KitVirtualDTO.KitVirtualRetorno>();

            foreach (var kit in listaKitsPossiveis)
            {
                switch (kit.TipoRegra)
                {
                    case TipoRegraKitEnum.RD01:
                        ProcessarRegraRD01(kit, ref listaProdutos, ref kitsFormados);
                        break;
                    case TipoRegraKitEnum.RD02:
                        ProcessarRegraRD02(kit, ref listaProdutos, ref kitsFormados);
                        break;
                    case TipoRegraKitEnum.RD03:
                        ProcessarRegraRD03(kit, ref listaProdutos, ref kitsFormados);
                        break;
                    case TipoRegraKitEnum.RD04:
                        ProcessarRegraRD04(kit, ref listaProdutos, ref kitsFormados);
                        break;
                    case TipoRegraKitEnum.RD05:
                        ProcessarRegraRD05(kit, ref listaProdutos, ref kitsFormados);
                        break;
                    case TipoRegraKitEnum.RD06:
                        ProcessarRegraRD06(kit, ref listaProdutos, ref kitsFormados);
                        break;
                    case TipoRegraKitEnum.RD07:
                        ProcessarRegraRD07(kit, ref listaProdutos, ref kitsFormados);
                        break;
                    case TipoRegraKitEnum.RD08:
                        ProcessarRegraRD08(kit, ref listaProdutos, ref kitsFormados);
                        break;
                    case TipoRegraKitEnum.RD09:
                        ProcessarRegraRD09(kit, ref listaProdutos, ref kitsFormados);
                        break;
                    default:
                        break;
                }
            }
            return kitsFormados;
        }

        private void ProcessarRegraRD01(KitVirtualDTO.KitVirtualEstruturado kit, ref List<KitVirtualDTO.Produto> listaProdutos, ref List<KitVirtualDTO.KitVirtualRetorno> kitsFormados)
        {
            List<KitVirtualDTO.Produto> listaProdutosCompativeis = new List<KitVirtualDTO.Produto>();
            int contadorProdutosCompativeis = 0;

            foreach (var produto in listaProdutos)
            {
                foreach (var listaA in kit.ListaProdutosA)
                {
                    if (produto.CodigoProduto == listaA.CodigoProduto)
                    {
                        produto.Valor = listaA.PrecoPraticadoAraujo;
                        listaProdutosCompativeis.Add(produto);
                        contadorProdutosCompativeis += produto.Quantidade;
                    }
                }
            }

            if (contadorProdutosCompativeis >= kit.QtdMinProdutoA) // existe pelo menos 1 kit
            {
                int qtdeKits = (int)(contadorProdutosCompativeis / kit.QtdMinProdutoA);

                KitVirtualDTO.KitVirtualRetorno kitFormado = new KitVirtualDTO.KitVirtualRetorno();
                kitFormado.CodigoKit = kit.CodigoKit;
                kitFormado.TipoRegra = (int)kit.TipoRegra;
                kitFormado.TipoAplicacao = (int)kit.TipoAplicacao;
                kitFormado.TituloKit = kit.TituloKit;
                kitFormado.DescricaoKit = kit.DescricaoKit;
                kitFormado.InicioVigencia = kit.InicioVigencia;
                kitFormado.FimVigencia = kit.FimVigencia;
                kitFormado.ValorIndenizacao = kit.ValorIndenizacao;
                kitFormado.ListaProdutos = new List<List<KitVirtualDTO.Produto>>();

                int prodAtual = 0;
                for (int idxQtdKit = 0; idxQtdKit < qtdeKits; idxQtdKit++)
                {
                    kitFormado.ListaProdutos.Add(new List<KitVirtualDTO.Produto>());

                    for (int idxProdKit = 0; idxProdKit < kit.QtdMinProdutoA; idxProdKit++)
                    {
                        if (listaProdutosCompativeis[prodAtual].Quantidade == 0)
                        {
                            prodAtual++;
                        }

                        kitFormado.ListaProdutos[idxQtdKit].Add(new KitVirtualDTO.Produto
                        {
                            CodigoProduto = listaProdutosCompativeis[prodAtual].CodigoProduto,
                            Valor = listaProdutosCompativeis[prodAtual].Valor,
                            Quantidade = 1,
                            Sequencial = idxQtdKit + 1
                        });

                        listaProdutosCompativeis[prodAtual].Quantidade--;
                    }
                }

                ProcessarRegraAplicacao(kit, ref kitFormado);

                kitsFormados.Add(kitFormado);
            }
        }

        private void ProcessarRegraRD02(KitVirtualDTO.KitVirtualEstruturado kit, ref List<KitVirtualDTO.Produto> listaProdutos, ref List<KitVirtualDTO.KitVirtualRetorno> kitsFormados)
        {
            List<KitVirtualDTO.Produto> listaProdutosCompativeisA = new List<KitVirtualDTO.Produto>();
            List<KitVirtualDTO.Produto> listaProdutosCompativeisB = new List<KitVirtualDTO.Produto>();

            int contadorProdutosCompativeisA = 0;
            int contadorProdutosCompativeisB = 0;

            foreach (var produto in listaProdutos)
            {
                foreach (var listaA in kit.ListaProdutosA)
                {
                    if (produto.CodigoProduto == listaA.CodigoProduto)
                    {
                        produto.Valor = listaA.PrecoPraticadoAraujo;
                        listaProdutosCompativeisA.Add(produto);
                        contadorProdutosCompativeisA += produto.Quantidade;
                    }
                }

                foreach (var listaB in kit.ListaProdutosB)
                {
                    if (produto.CodigoProduto == listaB.CodigoProduto)
                    {
                        produto.Valor = listaB.PrecoPraticadoAraujo;
                        listaProdutosCompativeisB.Add(produto);
                        contadorProdutosCompativeisB += produto.Quantidade;
                    }
                }
            }

            if (contadorProdutosCompativeisA >= kit.QtdMinProdutoA && contadorProdutosCompativeisB >= kit.QtdMinProdutoB) // existe pelo menos 1 kit
            {
                int qtdeKitsPossiveisA = (int)(contadorProdutosCompativeisA / kit.QtdMinProdutoA);
                int qtdeKitsPossiveisB = (int)(contadorProdutosCompativeisB / kit.QtdMinProdutoB);
                int qtdeKits = Math.Min(qtdeKitsPossiveisA, qtdeKitsPossiveisB);

                KitVirtualDTO.KitVirtualRetorno kitFormado = new KitVirtualDTO.KitVirtualRetorno();
                kitFormado.CodigoKit = kit.CodigoKit;
                kitFormado.TipoRegra = (int)kit.TipoRegra;
                kitFormado.TipoAplicacao = (int)kit.TipoAplicacao;
                kitFormado.TituloKit = kit.TituloKit;
                kitFormado.DescricaoKit = kit.DescricaoKit;
                kitFormado.InicioVigencia = kit.InicioVigencia;
                kitFormado.FimVigencia = kit.FimVigencia;
                kitFormado.ValorIndenizacao = kit.ValorIndenizacao;
                kitFormado.ListaProdutos = new List<List<KitVirtualDTO.Produto>>();


                int prodAtualA = 0;
                int prodAtualB = 0;
                for (int idxQtdKit = 0; idxQtdKit < qtdeKits; idxQtdKit++)
                {
                    kitFormado.ListaProdutos.Add(new List<KitVirtualDTO.Produto>());

                    for (int idxProdKitA = 0; idxProdKitA < kit.QtdMinProdutoA; idxProdKitA++)
                    {
                        if (listaProdutosCompativeisA[prodAtualA].Quantidade == 0)
                        {
                            prodAtualA++;
                        }

                        kitFormado.ListaProdutos[idxQtdKit].Add(new KitVirtualDTO.Produto
                        {
                            CodigoProduto = listaProdutosCompativeisA[prodAtualA].CodigoProduto,
                            Valor = listaProdutosCompativeisA[prodAtualA].Valor,
                            Quantidade = 1,
                            Sequencial = idxQtdKit + 1
                        });

                        listaProdutosCompativeisA[prodAtualA].Quantidade--;
                    }

                    for (int idxProdKitB = 0; idxProdKitB < kit.QtdMinProdutoB; idxProdKitB++)
                    {
                        if (listaProdutosCompativeisB[prodAtualB].Quantidade == 0)
                        {
                            prodAtualB++;
                        }

                        kitFormado.ListaProdutos[idxQtdKit].Add(new KitVirtualDTO.Produto
                        {
                            CodigoProduto = listaProdutosCompativeisB[prodAtualB].CodigoProduto,
                            Valor = listaProdutosCompativeisB[prodAtualB].Valor,
                            Quantidade = 1,
                            Sequencial = idxQtdKit + 1
                        });

                        listaProdutosCompativeisB[prodAtualB].Quantidade--;
                    }
                }

                ProcessarRegraAplicacao(kit, ref kitFormado);

                kitsFormados.Add(kitFormado);
            }
        }

        private void ProcessarRegraRD03(KitVirtualDTO.KitVirtualEstruturado kit, ref List<KitVirtualDTO.Produto> listaProdutos, ref List<KitVirtualDTO.KitVirtualRetorno> kitsFormados)
        {
            throw new NotImplementedException();
        }

        private void ProcessarRegraRD04(KitVirtualDTO.KitVirtualEstruturado kit, ref List<KitVirtualDTO.Produto> listaProdutos, ref List<KitVirtualDTO.KitVirtualRetorno> kitsFormados)
        {
            throw new NotImplementedException();
        }

        private void ProcessarRegraRD05(KitVirtualDTO.KitVirtualEstruturado kit, ref List<KitVirtualDTO.Produto> listaProdutos, ref List<KitVirtualDTO.KitVirtualRetorno> kitsFormados)
        {
            throw new NotImplementedException();
        }

        private void ProcessarRegraRD06(KitVirtualDTO.KitVirtualEstruturado kit, ref List<KitVirtualDTO.Produto> listaProdutos, ref List<KitVirtualDTO.KitVirtualRetorno> kitsFormados)
        {
            throw new NotImplementedException();
        }

        private void ProcessarRegraRD07(KitVirtualDTO.KitVirtualEstruturado kit, ref List<KitVirtualDTO.Produto> listaProdutos, ref List<KitVirtualDTO.KitVirtualRetorno> kitsFormados)
        {
            throw new NotImplementedException();
        }

        private void ProcessarRegraRD08(KitVirtualDTO.KitVirtualEstruturado kit, ref List<KitVirtualDTO.Produto> listaProdutos, ref List<KitVirtualDTO.KitVirtualRetorno> kitsFormados)
        {
            throw new NotImplementedException();
        }

        private void ProcessarRegraRD09(KitVirtualDTO.KitVirtualEstruturado kit, ref List<KitVirtualDTO.Produto> listaProdutos, ref List<KitVirtualDTO.KitVirtualRetorno> kitsFormados)
        {
            throw new NotImplementedException();
        }

        private void ProcessarRegraAplicacao(KitVirtualDTO.KitVirtualEstruturado kit, ref KitVirtualDTO.KitVirtualRetorno kitFormado)
        {
            switch (kit.TipoAplicacao)
            {
                case TipoAplicacaoKitEnum.ValorDescontoUltimoItem:
                    ProcessarTipoAplicacao_1(kit, ref kitFormado);
                    break;
                case TipoAplicacaoKitEnum.PercentualDescontoUltimoItem:
                    ProcessarTipoAplicacao_2(kit, ref kitFormado);
                    break;
                case TipoAplicacaoKitEnum.PercentualDescontoValorTotalItens:
                    ProcessarTipoAplicacao_3(kit, ref kitFormado);
                    break;
                case TipoAplicacaoKitEnum.UltimoItemGratuito:
                    ProcessarTipoAplicacao_4(kit, ref kitFormado);
                    break;
                case TipoAplicacaoKitEnum.QuantidadeYProdutosGratis:
                    ProcessarTipoAplicacao_5(kit, ref kitFormado);
                    break;
                case TipoAplicacaoKitEnum.PercentualDescontoProgressaoVenda:
                    ProcessarTipoAplicacao_6(kit, ref kitFormado);
                    break;
                case TipoAplicacaoKitEnum.PercentualDescontoDiferenciadoQtdeVendida:
                    ProcessarTipoAplicacao_7(kit, ref kitFormado);
                    break;
                default:
                    break;
            }
        }

        private void ProcessarTipoAplicacao_1(KitVirtualDTO.KitVirtualEstruturado kit, ref KitVirtualDTO.KitVirtualRetorno kitFormado) // Valor do desconto aplicado ao último item do kit
        {
            foreach (var subListaProduto in kitFormado.ListaProdutos)
            {
                if (kit.ValorDesconto >= subListaProduto[subListaProduto.Count - 1].Valor)
                {
                    subListaProduto[subListaProduto.Count - 1].Valor = 0.01;
                }
                else
                {
                    subListaProduto[subListaProduto.Count - 1].Valor -= kit.ValorDesconto;
                }
            }
        }

        private void ProcessarTipoAplicacao_2(KitVirtualDTO.KitVirtualEstruturado kit, ref KitVirtualDTO.KitVirtualRetorno kitFormado) // Percentual de desconto aplicado ao último item do kit
        {
            foreach (var subListaProduto in kitFormado.ListaProdutos)
            {
                double valorTotal = 0;
                foreach (var produto in subListaProduto)
                {
                    valorTotal += produto.Valor;
                }

                double desconto = valorTotal * kit.PercentualDesconto / 100;

                if (desconto >= subListaProduto[subListaProduto.Count - 1].Valor)
                {
                    subListaProduto[subListaProduto.Count - 1].Valor = 0.01;
                }
                else
                {
                    subListaProduto[subListaProduto.Count - 1].Valor -= desconto;
                }
            }
        }

        private void ProcessarTipoAplicacao_3(KitVirtualDTO.KitVirtualEstruturado kit, ref KitVirtualDTO.KitVirtualRetorno kitFormado) // Percentual de desconto aplicado ao valor total dos itens do kit
        {
            foreach (var subListaProduto in kitFormado.ListaProdutos)
            {
                foreach (var produto in subListaProduto)
                {
                    produto.Valor = Math.Round(produto.Valor - (produto.Valor * kit.PercentualDesconto / 100), 2);
                }
            }
        }

        private void ProcessarTipoAplicacao_4(KitVirtualDTO.KitVirtualEstruturado kit, ref KitVirtualDTO.KitVirtualRetorno kitFormado) // Último item do kit gratuito
        {
            foreach (var subListaProduto in kitFormado.ListaProdutos)
            {
                subListaProduto[subListaProduto.Count - 2].Valor = Math.Round(subListaProduto[subListaProduto.Count - 2].Valor - 0.01, 2);
                subListaProduto[subListaProduto.Count - 1].Valor = 0.01;
            }
        }

        private void ProcessarTipoAplicacao_5(KitVirtualDTO.KitVirtualEstruturado kit, ref KitVirtualDTO.KitVirtualRetorno kitFormado) // Quantidade Y de produtos grátis
        {
            throw new NotImplementedException();
        }

        private void ProcessarTipoAplicacao_6(KitVirtualDTO.KitVirtualEstruturado kit, ref KitVirtualDTO.KitVirtualRetorno kitFormado) // Percentual de desconto conforme progressão de venda
        {
            throw new NotImplementedException();
        }

        private void ProcessarTipoAplicacao_7(KitVirtualDTO.KitVirtualEstruturado kit, ref KitVirtualDTO.KitVirtualRetorno kitFormado) // Percentual de desconto diferenciado para quantidade vendida
        {
            throw new NotImplementedException();
        }
    }
}