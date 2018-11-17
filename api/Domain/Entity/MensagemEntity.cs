using System;

namespace tccenter.api.Domain.Entity
{
    public class MensagemEntity
    {
        public int IdMensagem { get; set; }
        public int IdPublicacao { get; set; }
        public int IdUsuarioComentou { get; set; }
        public string DescMensagem { get; set; }
        public DateTime DataMensagem { get; set; }
        public UsuarioEntity Usuario { get; set; }
    }
}