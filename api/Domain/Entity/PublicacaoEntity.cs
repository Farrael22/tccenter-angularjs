using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccenter.api.Domain.Entity
{
    public class PublicacaoEntity
    {
        public int IdUsuario { get; set; }
        public int IdPublicacao { get; set; }
        public string TituloPublicacao { get; set; }
        public string LinkPublicacao { get; set; }
        public string DescPublicacao { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string ResultadoPublicacao { get; set; }
    }
}