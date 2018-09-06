using balcao.offline.api.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace balcao.offline.api.DTO
{
    public partial class KitVirtualDTO
    {
        /// <summary>
        /// DTO de Entrada da lista de produtos para verificar ocorrencia de kit
        /// </summary>
        public class Produto
        {
            public long CodigoProduto { get; set; }
            public int Quantidade { get; set; }
            public double Valor { get; set; }
            public int Sequencial { get; set; }
        }

        /// <summary>
        /// DTO de Retorno principal da pesquisa de kit
        /// </summary>
        public class KitVirtualRetorno
        {
            public string CodigoKit { get; set; }
            public int TipoRegra { get; set; }
            public int TipoAplicacao { get; set; }
            public string TituloKit { get; set; }
            public string DescricaoKit { get; set; }
            public DateTime InicioVigencia { get; set; }
            public DateTime FimVigencia { get; set; }
            public double ValorIndenizacao { get; set; }
            public List<List<Produto>> ListaProdutos { get; set; }
        }

        /// <summary>
        /// Dto de kits estruturados
        /// </summary>
        public class KitVirtualEstruturado
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

            public List<ProdutoKitVirtual> ListaProdutosA { get; set; }
            public List<ProdutoKitVirtual> ListaProdutosB { get; set; }
        }
        /// <summary>
        /// Dto de produtos do kit
        /// </summary>
        public class ProdutoKitVirtual
        {
            public long CodigoProduto { get; set; }
            public double PrecoPraticadoAraujo { get; set; }
            public TipoListaProdutoKitEnum ListaProduto { get; set; }
            public double CustoNegociado { get; set; }
            public bool IndicadorProdutoIdenizado { get; set; }
        }
    }
}