using System.Collections.Generic;
using tccenter.api.Domain.DTO;

namespace tccenter.api.Business.TopicosInteressantes
{
    public interface ITopicosInteressantesBusiness
    {
        IEnumerable<TopicosInteressantesDTO> ObterTopicosInteressantes();
    }
}