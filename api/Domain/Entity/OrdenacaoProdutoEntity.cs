using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace balcao.offline.api.Entity
{
    public class OrdenacaoProdutoEntity
    {
        public long CD_PRODUTO { get; set; }
        public int NumLinha { get; set; }
        public int CARAC { get; set; }
    }    
}