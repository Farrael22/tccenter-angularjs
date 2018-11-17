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

        public IEnumerable<OrientadorEntity> ObterOrientadorPorPublicacao(int idOrientador)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<OrientadorEntity>(OrientadorQueries.OBTER_ORIENTADOR_POR_PUBLICACAO,
                    new
                    {
                        IdOrientador = idOrientador
                    });
            }
        }
    }
}