using System.Collections.Generic;
using tccenter.api.DataAccess.Queries;
using tccenter.api.Domain.Entity;
using tccenter.api.Helpers.DataAccess;

namespace tccenter.api.DataAccess.Repository.TopicosInteressantes
{
    public class TopicosInteressantesRepository : ITopicosInteressantesRepository
    {
        public IEnumerable<TopicosInteressantesEntity> ObterTopicosInteressantes()
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<TopicosInteressantesEntity>(TopicosInteressantesQueries.OBTER_TOPICOS_INTERESSANTES);
            }
        }

        public IEnumerable<TopicosInteressantesEntity> ObterTopicosInteressantesPorUsuario(int idUsuario)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<TopicosInteressantesEntity>(TopicosInteressantesQueries.OBTER_TOPICOS_INTERESSANTES_POR_USUARIO,
                    new { IdUsuario = idUsuario });
            }
        }
    }
}