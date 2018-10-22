using System.Collections.Generic;
using tccenter.api.DataAccess.Queries;
using tccenter.api.Domain.Entity;
using tccenter.api.Helpers.DataAccess;

namespace tccenter.api.DataAccess.Repository.TopicoMestre
{
    public class TopicoMestreRepository : ITopicoMestreRepository
    {
        public IEnumerable<TopicoMestreEntity> ObterTopicosMestre()
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<TopicoMestreEntity>(TopicoMestreQueries.OBTER_TOPICOS_MESTRE);
            }
        }
    }
}