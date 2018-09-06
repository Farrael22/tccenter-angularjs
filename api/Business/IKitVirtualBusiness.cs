using System.Collections.Generic;
using balcao.offline.api.DTO;

namespace balcao.offline.api.Business
{
    public interface IKitVirtualBusiness
    {
        IEnumerable<KitVirtualDTO.KitVirtualRetorno> ObterKitsVirtuais(List<KitVirtualDTO.Produto> listaProdutos);
    }
}
