using System.ComponentModel;

namespace balcao.offline.api.Domain.Enum
{
    public enum TipoAplicacaoKitEnum
    {
        [Description("Valor do desconto aplicado ao último item do kit")]
        ValorDescontoUltimoItem = 1,

        [Description("Percentual de desconto aplicado ao último item do kit")]
        PercentualDescontoUltimoItem = 2,

        [Description("Percentual de desconto aplicado ao valor total dos itens do kit")]
        PercentualDescontoValorTotalItens = 3,

        [Description("Último item do kit gratuito")]
        UltimoItemGratuito = 4,

        [Description("Quantidade Y de produtos grátis")]
        QuantidadeYProdutosGratis = 5,

        [Description("Percentual de desconto conforme progressão de venda")]
        PercentualDescontoProgressaoVenda = 6,

        [Description("Percentual de desconto diferenciado para quantidade vendida")]
        PercentualDescontoDiferenciadoQtdeVendida = 7
    }
}