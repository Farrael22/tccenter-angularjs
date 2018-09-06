using balcao.offline.api.Entity;
using System.Collections.Generic;
using balcao.offline.api.Helpers.DataAccess;

namespace balcao.offline.api.DataAccess.Repository
{
    public interface IKitVirtualRepository
    {
        IEnumerable<KitVirtualEntity> ListarKitsVirtuais(List<long> listaProdutos);
    }
}
