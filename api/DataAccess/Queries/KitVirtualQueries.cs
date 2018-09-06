using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace balcao.offline.api.DataAccess.Queries
{
    public class KitVirtualQueries
    {
        public static string OBTER_KITS_VIRTUAIS
        {
            get
            {
                return @"
                DECLARE @DATAATUAL VARCHAR(10)

                SET @DATAATUAL = CONVERT(char(10), getdate(), 126) 
                    SELECT * FROM
					(SELECT MAX(HDRDTHHOR) AS DataAtualizacao FROM BLOTBLPRMPRODUTOMESTRE_TMP (NOLOCK)) AS DT LEFT JOIN
                    (SELECT CKVCODKITVIRTUAL        AS CodigoKit
                           ,CKVDTHINIVIG            AS InicioVigencia
                           ,CKVDTHFIMVIG            AS FimVigencia
                           ,TRGSEQ                  AS TipoRegra
                           ,RADSEQREGRAAPLICACAO    AS TipoAplicacao
                           ,CKVNOMKITVIRTUAL        AS TituloKit
                           ,CKVDESKITVIRTUAL        AS DescricaoKit
                           ,CKVVALDESCONTO          AS ValorDesconto
                           ,CKVPERDESCONTO          AS PercentualDesconto
                           ,CKVNUMMAXKITCUPFIS      AS MaxPorCupom
                           ,CKVQTDMINPRODA          AS QtdMinProdutoA
                           ,CKVQTDMINPRODB          AS QtdMinProdutoB
                           ,CKVVALINDENIZACAOKIT    AS ValorIndenizacao
                           ,PRME_CD_PRODUTO         AS CodigoProduto
                           ,KPRIDCLISTAPROD         AS ListaProduto
                           ,KPRVALCUSTONEGOCIADO    AS CustoNegociado
                           ,KPRIDCPRODINDENIZADO    AS IndicadorProdutoIdenizado
                           ,KTVVALPRECOPRATICADO    AS PrecoPraticado
                           ,KTVVALPRECOTEMMAISARAUJO AS PrecoPraticadoTemMaisAraujo
                       FROM BLOTBLKTVKITVIRTUAL_TMP
                       WHERE PRME_CD_PRODUTO IN @PRODUTO
                    ) AS KIT ON 1 = 1
            ";
            }
        }
    }
}