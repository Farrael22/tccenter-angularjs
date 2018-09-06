using balcao.offline.api.DataAccess.Queries;
using balcao.offline.api.Helpers.DataAccess;
using balcao.offline.api.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace balcao.offline.api.DataAccess.Repository
{
    public class KitVirtualRepository : IKitVirtualRepository
    {
        public IEnumerable<KitVirtualEntity> ListarKitsVirtuais(List<long> listaProdutos)
        {
            using (var transaction = new TransactionHelperBaseGerente())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@PRODUTO", listaProdutos);

                return transaction.Query<KitVirtualEntity>(KitVirtualQueries.OBTER_KITS_VIRTUAIS, parametros);
            }
        }
    }
}