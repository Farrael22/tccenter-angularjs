using balcao.offline.api.Entity;
using System.Collections.Generic;
using balcao.offline.api.Helpers.DataAccess;

namespace balcao.offline.api.DataAccess.Repository
{
    public interface IProdutosRepository
    {
        IEnumerable<ProdutoEntity> ListarProdutosPorFonema(int filial, string strProduto, string fonema);
        IEnumerable<ProdutoEntity> ListarProdutosPorCodigoInterno(int filial, long codigo, int digito);
        IEnumerable<ProdutoEntity> ListarProdutosPorDescricao(int filial, string texto);
        IEnumerable<ProdutoEntity> ObterCodigoInternoProdutoPorCodigoBarra(string codigoBarra);
        IEnumerable<CodigoBarraEntity> ObterCodigoBarraPorCodigoInternoProduto(List<int> lista);
        IEnumerable<OrdenacaoProdutoEntity> ObterOrdenacaoProdutos(List<int> produtos);
        IEnumerable<int> ListarProdutosEquivalentes(int filial, int codigoProduto);
        IEnumerable<ProdutoEntity> ObterProdutosEquivalentes(int filial, List<int> listaCodigos);
    }
}
