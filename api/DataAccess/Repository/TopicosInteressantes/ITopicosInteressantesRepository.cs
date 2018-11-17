using System.Collections.Generic;
using tccenter.api.Domain.Entity;

namespace tccenter.api.DataAccess.Repository.TopicosInteressantes
{
    public interface ITopicosInteressantesRepository
    {
        IEnumerable<TopicosInteressantesEntity> ObterTopicosInteressantes(int idTopicoMestre);
        IEnumerable<TopicosInteressantesEntity> ObterTopicosInteressantesPorUsuario(int idUsuario);
        IEnumerable<TopicoMestreEntity> ObterTopicosMestrePorUsuario(int idUsuario);
        IEnumerable<TopicosInteressantesEntity> ObterTopicosInteressantesPorPublicacao(int idPublicacao);
    }
}