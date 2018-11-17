using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccenter.api.Domain.DTO
{
    public class MensagemDTO
    {
        public int IdMensagem { get; set; }
        public int IdPublicacao { get; set; }
        public int IdUsuarioComentou { get; set; }
        public string DescMensagem { get; set; }
        public DateTime DataMensagem { get; set; }
        public UsuarioDTO Usuario { get; set; }
    }
}