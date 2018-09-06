using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace balcao.offline.api.Entity
{
    public class CodigoBarraEntity
    {
        public long CodProduto { get; set; }
        public string Codigo { get; set; }
        public string Principal { get; set; }
    }
}