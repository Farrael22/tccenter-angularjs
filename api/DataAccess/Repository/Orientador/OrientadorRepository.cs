using System.Collections.Generic;
using tccenter.api.DataAccess.Queries;
using tccenter.api.Domain.Entity;
using tccenter.api.Helpers.DataAccess;

namespace tccenter.api.DataAccess.Repository.Orientador
{
    public class OrientadorRepository : IOrientadorRepository
    {
        public IEnumerable<OrientadorEntity> ObterOrientadores()
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<OrientadorEntity>(OrientadorQueries.BUSCAR_ORIENTADORES);
            }
        }
    }
}