using System.Collections.Generic;
using tccenter.api.DataAccess.Queries;
using tccenter.api.Domain.Entity;
using tccenter.api.Helpers.DataAccess;

namespace tccenter.api.DataAccess.Repository.TopicosInteressantes
{
    public class TopicosInteressantesRepository : ITopicosInteressantesRepository
    {
        public IEnumerable<TopicosInteressantesEntity> ObterTopicosInteressantes(int idTopicoMestre)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<TopicosInteressantesEntity>(TopicosInteressantesQueries.OBTER_TOPICOS_INTERESSANTES,
                    new { IdTopicoMestre = idTopicoMestre});
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

        public IEnumerable<TopicosInteressantesEntity> ObterTopicosInteressantesPorPublicacao(int idPublicacao)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<TopicosInteressantesEntity>(TopicosInteressantesQueries.OBTER_TOPICOS_INTERESSANTES_POR_PUBLICACAO,
                    new { IdPublicacao = idPublicacao });
            }
        }

        public IEnumerable<TopicoMestreEntity> ObterTopicosMestrePorUsuario(int idUsuario)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<TopicoMestreEntity>(TopicosInteressantesQueries.OBTER_TOPICOS_MESTRE_POR_USUARIO,
                    new { IdUsuario = idUsuario });
            }
        }

    }
}