using balcao.offline.api.Entity;
using balcao.offline.api.DataAccess.Queries;
using System.Collections.Generic;
using System.Configuration;
using balcao.offline.api.Helpers.DataAccess;

namespace balcao.offline.api.DataAccess.Repository
{
    public class ProdutosRepository : IProdutosRepository
    {

        public IEnumerable<ProdutoEntity> ListarProdutosPorCodigoInterno(int filial, long codigo, int digito)
        {
            using (var transaction = new TransactionHelperBaseGerente())
            {
                return transaction.Query<ProdutoEntity>(ProdutosQueries.OBTER_PRODUTOS_POR_CODIGOINTERNO,
                    new { Filial = filial, Produto = codigo, Digito = digito });
            }
        }

        public IEnumerable<ProdutoEntity> ListarProdutosPorFonema(int filial, string descricaoProduto, string fonema)
        {
            using (var transaction = new TransactionHelperBaseGerente())
            {
                return transaction.Query<ProdutoEntity>(ProdutosQueries.OBTER_PRODUTOS_POR_FONEMA,
                    new { Filial = filial, STRPRODUTO = descricaoProduto, STRFONEMA = fonema });
            }
        }

        public IEnumerable<ProdutoEntity> ListarProdutosPorDescricao(int filial, string descricaoProduto)
        {
            using (var transaction = new TransactionHelperBaseGerente())
            {
                return transaction.Query<ProdutoEntity>(ProdutosQueries.OBTER_PRODUTOS_POR_DESCRICAO,
                    new { STRPRODUTO = descricaoProduto });
            }
        }

        public IEnumerable<ProdutoEntity> ObterCodigoInternoProdutoPorCodigoBarra(string codigoBarra)
        {
            using (var transaction = new TransactionHelperBaseGerente())
            {
                return transaction.Query<ProdutoEntity>(ProdutosQueries.OBTER_CODIGOINTERNO_POR_CODIGOBARRA,
                    new { CODIGOBARRA = codigoBarra });
            }
        }

        public IEnumerable<OrdenacaoProdutoEntity> ObterOrdenacaoProdutos(List<int> produtos)
        {
            using (var transaction = new TransactionHelperBaseGerente())
            {
                return transaction.Query<OrdenacaoProdutoEntity>(ProdutosQueries.OBTER_ORDENACAO_POR_CODIGO_INTERNO,
                    new { Produtos = produtos });
            }
        }

        public IEnumerable<CodigoBarraEntity> ObterCodigoBarraPorCodigoInternoProduto(List<int> listaCodigoProdutos)
        {
            using (var transaction = new TransactionHelperBaseGerente())
            {
                return transaction.Query<CodigoBarraEntity>(ProdutosQueries.OBTER_CODIGOBARRA_POR_CODIGOINTERNO,
                    new { Produtos = listaCodigoProdutos });
            }
        }

        public IEnumerable<int> ListarProdutosEquivalentes(int filial, int codigoProduto)
        {
            using (var transaction = new TransactionHelperBaseGerente())
            {
                return transaction.Query<int>(ProdutosQueries.OBTER_LISTA_CODIGOS_EQUIVALENTES,
                    new { Cod = codigoProduto });
            }
        }

        public IEnumerable<ProdutoEntity> ObterProdutosEquivalentes(int filial, List<int> listaCodigos)
        {
            using (var transaction = new TransactionHelperBaseGerente())
            {
                return transaction.Query<ProdutoEntity>(ProdutosQueries.OBTER_PRODUTOS_EQUIVALENTES,
                    new { Filial = filial, Produtos = listaCodigos });
            }
        }
    }
}
