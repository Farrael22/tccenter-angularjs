namespace balcao.offline.api.DataAccess.Queries
{
    public class EstacaoQueries
    {
        public static string OBTER_ESTACAO_POR_IP
        {
            get
            {
                return @"
                    SELECT * FROM
					(SELECT MAX(HDRDTHHOR) AS DataAtualizacao FROM BLOTBLPRMPRODUTOMESTRE_TMP (NOLOCK)) AS DT LEFT JOIN
                    (SELECT  
                        FIEC_NR_ECF         AS Estacao,
                        FIEC_DS_ECF         AS NomeEstacao,
                        FILI_CD_FILIAL      AS Filial,
                        FILI_NM_FANTASIA    AS NomeFilial,
                        REPLACE(REPLACE(FIEC_NR_IP,'.00','.'),'.0','.')     AS Ip,
                        FLESITSITUACAO      AS Situacao
                    FROM BLOTBLFLEFILIALESTACAO_TMP
	                WHERE REPLACE(REPLACE(FIEC_NR_IP,'.00','.'),'.0','.') = 
                        REPLACE(REPLACE(@IP,'.00','.'),'.0','.')
					) AS Estacoes ON 1 = 1
            ";
            }
        }
    }
}
