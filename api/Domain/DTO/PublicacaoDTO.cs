using System;
using System.Collections.Generic;

namespace tccenter.api.Domain.DTO
{
    public class PublicacaoDTO
    {
        public int IdPublicacao { get; set; }
        public int IdUsuario { get; set; }
        public string TituloPublicacao { get; set; }
        public string DescPublicacao { get; set; }
        public string ResultadoPublicacao { get; set; }
        public string LinkPublicacao { get; set; }
        public DateTime DataPublicacao { get; set; }
        public TopicosInteressantesDTO TopicoInteresse { get; set; }
        public OrientadorDTO Orientador { get; set; }
        public List<MensagemDTO> Mensagens { get; set; }
    }
}