using balcao.offline.api.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace balcao.offline.api.Entity
{
    public class KitVirtualEntity
    {
        public string CodigoKit { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FimVigencia { get; set; }
        public TipoRegraKitEnum TipoRegra { get; set; }
        public TipoAplicacaoKitEnum TipoAplicacao { get; set; }
        public string TituloKit { get; set; }
        public string DescricaoKit { get; set; }
        public double ValorDesconto { get; set; }
        public double PercentualDesconto { get; set; }
        public int MaxPorCupom { get; set; }
        public int QtdMinProdutoA { get; set; }
        public int QtdMinProdutoB { get; set; }
        public double ValorIndenizacao { get; set; }

        public long CodigoProduto { get; set; }
        public string ListaProduto { get; set; }
        public double CustoNegociado { get; set; }
        public bool IndicadorProdutoIdenizado { get; set; }
        public double PrecoPraticado { get; set; }
        public double PrecoPraticadoTemMaisAraujo { get; set; }

        public DateTime DataAtualizacao { get; set; }
    }
}