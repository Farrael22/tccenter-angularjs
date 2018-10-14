using AutoMapper;
using System.Collections.Generic;
using tccenter.api.DataAccess.Repository.TopicosInteressantes;
using tccenter.api.Domain.DTO;
using tccenter.api.Domain.Entity;

namespace tccenter.api.Business.TopicosInteressantes
{
    public class TopicosInteressantesBusiness : ITopicosInteressantesBusiness
    {
        private readonly ITopicosInteressantesRepository _loginRepository;
        public TopicosInteressantesBusiness(ITopicosInteressantesRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public IEnumerable<TopicosInteressantesDTO> ObterTopicosInteressantes()
        {
            var listaTopicosEntity = _loginRepository.ObterTopicosInteressantes();

            return Mapper.Map<IEnumerable<TopicosInteressantesEntity>, IEnumerable<TopicosInteressantesDTO>>(listaTopicosEntity);
        }
    }
}