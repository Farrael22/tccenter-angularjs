using System.Collections.Generic;
using tccenter.api.Domain.Entity;

namespace tccenter.api.DataAccess.Repository.TopicosInteressantes
{
    public interface ITopicosInteressantesRepository
    {
        IEnumerable<TopicosInteressantesEntity> ObterTopicosInteressantes();
        IEnumerable<TopicosInteressantesEntity> ObterTopicosInteressantesPorUsuario(int idUsuario);
    }
}