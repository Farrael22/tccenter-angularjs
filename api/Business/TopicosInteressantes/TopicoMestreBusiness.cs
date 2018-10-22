using AutoMapper;
using System.Collections.Generic;
using tccenter.api.DataAccess.Repository.TopicoMestre;
using tccenter.api.DataAccess.Repository.TopicosInteressantes;
using tccenter.api.Domain.DTO;
using tccenter.api.Domain.Entity;

namespace tccenter.api.Business.TopicosInteressantes
{
    public class TopicoMestreBusiness : ITopicoMestreBusiness
    {
        private readonly ITopicosInteressantesRepository _topicosRepository;
        private readonly ITopicoMestreRepository _topicoMestreRepository;

        public TopicoMestreBusiness(ITopicosInteressantesRepository loginRepository, ITopicoMestreRepository topicoMestreRepository)
        {
            _topicosRepository = loginRepository;
            _topicoMestreRepository = topicoMestreRepository;
        }

        public IEnumerable<TopicoMestreDTO> ObterTopicosInteressantes()
        {
            var listaTopicosMestreEntity = _topicoMestreRepository.ObterTopicosMestre();
            var listaTopicosMestre = Mapper.Map<IEnumerable<TopicoMestreDTO>>(listaTopicosMestreEntity);

            foreach (var item in listaTopicosMestre)
            {
                item.TopicosInteressantes = new List<TopicosInteressantesDTO>();
                var listaTopicosEntity = _topicosRepository.ObterTopicosInteressantes(item.IdTopicoMestre);

                foreach (var topico in listaTopicosEntity)
                {
                    item.TopicosInteressantes.Add(Mapper.Map<TopicosInteressantesDTO>(topico));
                }
            }

            return listaTopicosMestre;
        }
    }
}