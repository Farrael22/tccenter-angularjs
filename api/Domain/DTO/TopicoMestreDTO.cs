using System.Collections.Generic;

namespace tccenter.api.Domain.DTO
{
    public class TopicoMestreDTO
    {
        public int IdTopicoMestre { get; set; }
        public string DescricaoTopicoMestre { get; set; }
        public List<TopicosInteressantesDTO> TopicosInteressantes { get; set; }
    }
}