using System.Collections.Generic;
using balcao.offline.api.DTO;

namespace balcao.offline.api.Business
{
    public interface IProdutosBusiness
    {
        IEnumerable<ProdutoDTO> Busca(int filial, string texto);
        IEnumerable<ProdutoDTO> Equivalentes(int filial, int codigoProduto);
    }
}