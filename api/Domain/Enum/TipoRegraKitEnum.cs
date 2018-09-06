using System.ComponentModel;

namespace balcao.offline.api.Domain.Enum
{
    public enum TipoRegraKitEnum
    {
        [Description("O kit é formado por uma quantidade de produtos da lista.")]
        RD01 = 1,

        [Description("O kit é formado por uma quantidade de produtos de uma lista A + uma quantidade de produtos de uma lista B.")]
        RD02 = 2,

        [Description("O kit é formado por uma quantidade de produtos de uma lista A + uma unidade de um produto de uma lista B.")]
        RD03 = 3,

        [Description("O kit é formado por uma quantidade de produtos de uma lista A + uma quantidade de produtos de uma lista B + uma unidade de um produto de uma lista C.")]
        RD04 = 4,

        [Description("O kit é formado pelo valor da soma de produtos da lista.")]
        RD05 = 5,

        [Description("O kit é formado pelo valor da soma de produtos de uma lista A + uma unidade de um produto de uma lista B.")]
        RD06 = 6,

        [Description("Leve X Pague Y - Uma lista de produtos.")]
        RD07 = 21,

        [Description("Desconto Diferenciado - Uma lista de produtos.")]
        RD08 = 22,

        [Description("Desconto Progressivo - Uma lista de produtos.")]
        RD09 = 23
    }
}