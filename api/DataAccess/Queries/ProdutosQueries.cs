namespace balcao.offline.api.DataAccess.Queries
{
    public static class ProdutosQueries
    {

        public static string QUERY_PADRAO
        {
            get
            {
                return @"
                    DECLARE @DATAATUAL VARCHAR(10)

                    SET @DATAATUAL = CONVERT(char(10), getdate(), 126) 
                    
                    SELECT * FROM
					(SELECT MAX(HDRDTHHOR) AS DataAtualizacao FROM BLOTBLPRMPRODUTOMESTRE_TMP (NOLOCK)) AS DT LEFT JOIN
                    (SELECT TOP 300
                    	DT.DataAtualizacao      AS DataAtualizacao,
                        PRME_CD_PRODUTO         AS Codigo,
                        PRME_NR_DV              AS Digito,
                        PRME_TX_DESCRICAO1      AS DescricaoResumida,
                        PRME_TX_DESCRICAO2      AS DescricaoCompleta,
                        PRMVALPRECODE           AS PrecoDe,
                        PRMVALPRECOPOR          AS PrecoPor,
                        PRMSITSITUACAO          AS Situacao,        
                        FORN_NM_RAZSOCIAL       AS Fabricante,
                        TPLP_SG_PSICO           AS TipoReceita,
                        PRMDESCLASSIFICACAO     AS Classificacao,       
                        EMBA_SG_VENDA           AS TipoEmbalagem,
                        PRMNUMINDICEGELADEIRA   AS IndicGeladeira,
                        PRMNUMIDCEEXCLUSIVOFP   AS IndicExclusivoFP,
                        PRMNUMINDICEFP          AS IndicFP,
                        PRMNUMINDICEUSOCONT     AS IndicUsoContinuo,
                        PRMVALPRECOFP           AS PrecoFP,
	                    PRMI_QT_APRESENTACAO    AS QuantidadeEmbalagem,
	                    PRMQTDEMBALAGEMFP       AS QuantidadeEmbalagemFP,
	                    PRDS_QT_DIAS            AS QuantidadeDiasFP,
	                    PRDS_QT_UNIDADE         AS QuantidadeUnidadesFP,
                        PRMINDINDICERECEITA     AS IndicREC,
                        PRMINDANTIBIOTICO       AS IndicAntibiotico,
                        PRMINDPSICOTROPICO      AS IndicPsicotropico,
                        RMINDINDICEPBM          AS IndicPBM,
                        PRMDESCATEGORIA         AS Categoria,
                        PRMDESSUBCATEGORIA      AS SubCategoria,
                        PRMIDCPRIORIDADEVENDA   AS PrioridadeVenda,
                        PRMORDORDENACAO         AS Ordenacao,
                        PRMI_TX_COMPOSICAO      AS PrincipioAtivo,
                        TPLP_QT_DIASTRAT        AS PeriodoTratamento,
                        PRMINDINTERCAMBIAVEL    AS IndicIntercambiavel,
                        PRMVALPRECOCRM          AS ValorPrecoCRM, 
                        PRMIDCKITEXCLUSIVOCRM   AS KitExclusivoCRM,
                        PRMDESDINAMICAKIT       AS DinamicaKit,	
                        TRGCODTIPOREGRA         AS TipoRegraKit,
                        PRMI_DESC_REDUZ_FONEMA  AS FonemaDescricaoReduzida,
                        PRMI_DESC_COMP_FONEMA   AS FonemaDescricaoCompleta,
                        PRMI_PRIN_ATIV_FONEMA   AS FonemaPrincipioAtivo,
                        PRMI_APELIDO_FONEMA     AS FonemaApelido,
                        PRMI_TX_COMPOSICAO_FONEMA AS FonemaComposicao,
                        PRMI_FL_CLASSIFICACAO   AS ClassificacaoEquivalente,
                        PRMINDPRIORIDADEVENDA   AS IndicPrioridadeVenda,
                        PRDP_FL_SITUACAO        AS SituacaoDeposito,
                        FILI_CD_FILIAL          AS Filial
                    FROM
                        (SELECT MAX(HDRDTHHOR) AS DataAtualizacao FROM BLOTBLPRMPRODUTOMESTRE_TMP (NOLOCK)) AS DT LEFT JOIN
	                    BLOTBLPRMPRODUTOMESTRE_TMP PM (NOLOCK) on 1 = 1
                    WHERE 1 = 1 /*
                        (CKVDTHINIVIG <= (@DATAATUAL + 'T23:59:59') 
                        AND CKVDTHFIMVIG >= (@DATAATUAL + 'T00:00:00'))*/"; //TODO BUSCA EM DATA ATUAL
            }
        }

        public static string OBTER_PRODUTOS_POR_CODIGOINTERNO
        {
            get
            {
                return QUERY_PADRAO + CRITERIO_POR_CODIGOINTERNO;
            }
        }

        public static string OBTER_PRODUTOS_POR_DESCRICAO
        {
            get
            {
                return QUERY_PADRAO + CRITERIO_POR_DESCRICAO;
            }
        }

        public static string OBTER_PRODUTOS_POR_FONEMA
        {
            get
            {
                return QUERY_PADRAO + CRITERIO_POR_FONEMA;
            }
        }                

        private static string CRITERIO_POR_FONEMA
        {
            get
            {
                return @"  
                    AND (PRME_TX_DESCRICAO1 LIKE @STRPRODUTO 
                    OR PRMI_APELIDO_FONEMA LIKE '%' + @STRFONEMA + '%' 
                    OR PRMI_DESC_REDUZ_FONEMA like @STRFONEMA + ' %' 
                    OR PRMI_DESC_REDUZ_FONEMA like '% ' + @STRFONEMA + ' %' 
                    OR PRMI_DESC_REDUZ_FONEMA like '% ' + @STRFONEMA + ' ' 
                    OR PRMI_DESC_REDUZ_FONEMA like '%' + @STRFONEMA + '%' 
                    OR PRMI_DESC_COMP_FONEMA like '%' + @STRFONEMA + '%' 
                    OR PRMI_TX_COMPOSICAO_FONEMA like '%' + @STRFONEMA + '%')
                ORDER BY  
                    CASE 
                        WHEN PRME_TX_DESCRICAO1 LIKE @STRPRODUTO THEN '0'
                        WHEN PRMI_DESC_REDUZ_FONEMA like @STRFONEMA + ' %' THEN '01'		 
				        WHEN PRMI_DESC_REDUZ_FONEMA like '% ' + @STRFONEMA + ' %' THEN '02'
				        WHEN PRMI_DESC_REDUZ_FONEMA like '% ' + @STRFONEMA + ' ' THEN '03'
				        WHEN PRMI_DESC_REDUZ_FONEMA like '%' + @STRFONEMA + '%' THEN '04'
				        WHEN PRMI_DESC_COMP_FONEMA like '%' + @STRFONEMA + '%' THEN '05'
				        WHEN PRMI_TX_COMPOSICAO_FONEMA like '%' + @STRFONEMA + '%' THEN '06'	 
				        WHEN PRMI_APELIDO_FONEMA like '%' + @STRFONEMA + '%' THEN '07'		
				        ELSE '08' 
                    END
                    			        	) AS Produtos ON 1 = 1
";
            }
        }

        private static string CRITERIO_POR_DESCRICAO
        {
            get
            {
                return @" AND PRME_TX_DESCRICAO1 LIKE @STRPRODUTO
                            				) AS Produtos ON 1 = 1";
            }
        }

        private static string CRITERIO_POR_CODIGOINTERNO
        {
            get
            {
                return @" AND PRME_CD_PRODUTO = @Produto AND PRME_NR_DV = @Digito
                            				) AS Produtos ON 1 = 1";
            }
        }

        public static string OBTER_CODIGOBARRA_POR_CODIGOINTERNO
        {
            get
            {
                return @"SELECT 
		                PRME_CD_PRODUTO AS CodProduto,
		                        RIGHT(CM.COBM_CD_BARRA,13) AS Codigo,
                                COBM_FL_PRINCIPAL AS Principal
                        FROM BLOTBLCBMCODIGOBARRAMESTRE_TMP CM(NOLOCK) 
                        WHERE ((COBM_FL_PRINCIPAL = 'S' AND COBM_TP_INTEXT <> 'I') OR COBM_TP_INTEXT = 'E')
                        AND CM.PRME_CD_PRODUTO in @Produtos ORDER BY COBM_FL_PRINCIPAL            	    
                ";
            }
        }

        public static string OBTER_CODIGOINTERNO_POR_CODIGOBARRA
        {
            get
            {
                return @"
                    SELECT TOP 1 CAST(PM.PRME_CD_PRODUTO AS VARCHAR)+''+CAST(PM.PRME_NR_DV AS VARCHAR) as CodigoInterno
                    FROM BLOTBLCBMCODIGOBARRAMESTRE_TMP CB (NOLOCK) 
                    INNER JOIN dbo.BLOTBLPRMPRODUTOMESTRE_TMP PM (NOLOCK) ON CB.PRME_CD_PRODUTO = PM.PRME_CD_PRODUTO
                    WHERE CB.COBM_TP_CODBARRA = 'V' AND CB.COBM_CD_BARRA  = @CODIGOBARRA";
            }
        }

        public static string OBTER_ORDENACAO_POR_CODIGO_INTERNO
        {
            get
            {
                return @"
                        BEGIN

	                        set NOCOUNT ON
	                        set ARITHABORT ON
	                        declare @PRIMEIRO INT
	                        set @PRIMEIRO = 1

	                        DECLARE @PRME_CD_PRODUTO int,
			                        @SPANOMSUB varchar(100),
			                        @CPMQTDUND numeric(12,5),
			                        @UNDNOM varchar(100),
			                        @APMNOM varchar(300),
			                        @PRMI_QT_APRESENTACAO numeric(8,3),

			                        @CONCAT_PRME_CD_PRODUTO int,
			                        @CONCAT_SPANOMSUB varchar(850),
			                        @CONCAT_CPMQTDUND varchar(300),
			                        @CONCAT_UNDNOM varchar(300),
			                        @CONCAT_APMNOM varchar(300),
			                        @CONCAT_PRMI_QT_APRESENTACAO varchar(300)
			
			
		                        IF OBJECT_ID('tempdb..#CONCATENADOS') IS NOT NULL     --Remove dbo here 
    		                        DROP TABLE #CONCATENADOS   
		
		                        CREATE TABLE #CONCATENADOS (
				                        CD_PRODUTO int,
				                        NOMSUB varchar(850),
				                        QTDUND varchar(300),
				                        UNDNOM varchar(300),
				                        APMNOM varchar(300),
				                        QT_APRES varchar(300)
		                        )
		
		                        CREATE NONCLUSTERED INDEX [IX_CONCATENADOS] ON dbo.#CONCATENADOS (CD_PRODUTO ASC, NOMSUB ASC)
		                        WITH (  PAD_INDEX  = OFF, 
				                        STATISTICS_NORECOMPUTE  = OFF, 
				                        SORT_IN_TEMPDB = OFF, 
				                        IGNORE_DUP_KEY = OFF, 
				                        DROP_EXISTING = OFF, 
				                        ONLINE = OFF, 
				                        ALLOW_ROW_LOCKS  = ON, 
				                        ALLOW_PAGE_LOCKS  = ON, 
				                        FILLFACTOR = 75 )


	                        DECLARE prod_principio CURSOR FOR 
		                        SELECT
			                        PRME_CD_PRODUTO,
			                        SPANOMSUB,
			                        CPMQTDUND,
			                        UNDNOM,
			                        APMNOM,
			                        PRMI_QT_APRESENTACAO
		                        FROM
			                        BLOTBLPCMPRODCOMPOSICAO_TMP (NOLOCK)
		                        WHERE
			                        PRME_CD_PRODUTO IN @Produtos
		                        ORDER BY
			                        PRME_CD_PRODUTO,
			                        SPANOMSUB

	                        OPEN prod_principio
	                        SET @CONCAT_PRME_CD_PRODUTO = 0
	                        SET @CONCAT_SPANOMSUB = ';'
	                        SET @CONCAT_CPMQTDUND = ';'
	                        SET @CONCAT_UNDNOM = ';'
	                        SET @CONCAT_APMNOM = ';'
	                        SET @CONCAT_PRMI_QT_APRESENTACAO = ';'

	                        -- Busca o primeiro registro e armazena em variaveis.
	                        -- Nota: as variaveis estao na mesma ordem das colunas do select

	                        FETCH NEXT FROM prod_principio
	                        INTO @PRME_CD_PRODUTO,@SPANOMSUB,@CPMQTDUND,@UNDNOM,@APMNOM,@PRMI_QT_APRESENTACAO


	                        WHILE @@FETCH_STATUS = 0
	                        BEGIN

		                        IF @CONCAT_PRME_CD_PRODUTO = @PRME_CD_PRODUTO
			                        OR @PRIMEIRO = 1
			                        BEGIN
				                        SET @PRIMEIRO = 0
				                        SET @CONCAT_PRME_CD_PRODUTO = @PRME_CD_PRODUTO
				                        SET @CONCAT_SPANOMSUB = @CONCAT_SPANOMSUB + @SPANOMSUB + ';'
				                        SET @CONCAT_CPMQTDUND = @CONCAT_CPMQTDUND + right(replicate('0',15) + ltrim(cast(@CPMQTDUND as varchar(15))),15) + ';'
				                        SET @CONCAT_UNDNOM = @CONCAT_UNDNOM + @UNDNOM + ';'
				                        SET @CONCAT_APMNOM = @CONCAT_APMNOM + @APMNOM + ';'
				                        SET @CONCAT_PRMI_QT_APRESENTACAO = @CONCAT_PRMI_QT_APRESENTACAO + right(replicate('0',15) + ltrim(cast(isnull(@PRMI_QT_APRESENTACAO,99999) as varchar(15))),15) + ';'
			                        END
		                        ELSE
			                        BEGIN
				                        if @CONCAT_SPANOMSUB is null
					                        insert into #CONCATENADOS values (
						                        @CONCAT_PRME_CD_PRODUTO,
						                        'z',
						                        'z',
						                        'z',
						                        'z',
						                        'z')
				                        else
					                        insert into #CONCATENADOS values (
						                        @CONCAT_PRME_CD_PRODUTO,
						                        @CONCAT_SPANOMSUB,
						                        @CONCAT_CPMQTDUND,
						                        @CONCAT_UNDNOM,
						                        @CONCAT_APMNOM,
						                        @CONCAT_PRMI_QT_APRESENTACAO)

				                        SET @CONCAT_PRME_CD_PRODUTO = @PRME_CD_PRODUTO
				                        SET @CONCAT_SPANOMSUB = ';' + @SPANOMSUB + ';'
				                        SET @CONCAT_CPMQTDUND = ';' + right(replicate('0',15) + ltrim(cast(@CPMQTDUND as varchar(15))),15) + ';'
				                        SET @CONCAT_UNDNOM = ';' + @UNDNOM + ';'
				                        SET @CONCAT_APMNOM = ';' + @APMNOM + ';'
				                        SET @CONCAT_PRMI_QT_APRESENTACAO = ';' + right(replicate('0',15) + ltrim(cast(isnull(@PRMI_QT_APRESENTACAO,99999) as varchar(15))),15) + ';'
			                        END

	                           FETCH NEXT FROM prod_principio
	                           INTO @PRME_CD_PRODUTO,@SPANOMSUB,@CPMQTDUND,@UNDNOM,@APMNOM,@PRMI_QT_APRESENTACAO

	                        END

	                        if @CONCAT_PRME_CD_PRODUTO <> 0
		                        insert into #CONCATENADOS values (
			                        @CONCAT_PRME_CD_PRODUTO,
			                        @CONCAT_SPANOMSUB,
			                        @CONCAT_CPMQTDUND,
			                        @CONCAT_UNDNOM,
			                        @CONCAT_APMNOM,
			                        @CONCAT_PRMI_QT_APRESENTACAO)

	                        CLOSE prod_principio
	                        DEALLOCATE prod_principio

	                        select
		                        CD_PRODUTO,	
		                        CARAC,
		                        ROW_NUMBER() OVER(ORDER BY SEMCOR,NOMSUB,QTDUND,UNDNOM,APMNOM,QT_APRES,CARAC,PRME_TX_DESCRICAO1) AS 'NumLinha'
	                        FROM
	                        (
	                        select 
		                        con.CD_PRODUTO,
		                        (case when PM.PRMINDPRIORIDADEVENDA = 999 then 1 else 0 end) as SEMCOR,
		                        PM.PRMINDPRIORIDADEVENDA as CARAC,
                                (case when NOMSUB = 'z' then 0 else 1 end) as TEMPRINCIPIO,
		                        PM.PRDP_FL_SITUACAO as FL_SITUACAO,
		                        NOMSUB,QTDUND,UNDNOM,APMNOM,QT_APRES,PM.PRME_TX_DESCRICAO1
	                        from 
		                        #CONCATENADOS con (nolock)
		                        INNER JOIN BLOTBLPRMPRODUTOMESTRE_TMP PM (NOLOCK) ON PM.PRME_CD_PRODUTO = con.CD_PRODUTO
	                        ) tmp

	                        drop table #CONCATENADOS
	
                        end
                ";
            }
        }

        public static string OBTER_LISTA_CODIGOS_EQUIVALENTES
        {
            get
            {
                return @"
                        SELECT PRODUTOSEQUIVALENTES.PRME_CD_PRODUTO as Codigo FROM 
                        (
	                        SELECT DISTINCT
		                        PE.PRME_CD_PRODUTO 
	                        FROM 
		                        BLOTBLPCMPRODCOMPOSICAO_TMP (NOLOCK) AS PE LEFT JOIN
		                        (
			                        SELECT * 
			                        FROM BLOTBLPCMPRODCOMPOSICAO_TMP (NOLOCK) 
			                        WHERE PRME_CD_PRODUTO = @Cod
		                        ) AS PROD ON
			                        PROD.SPACOD = PE.SPACOD 
			                        AND PROD.UNDCOD = PE.UNDCOD 
			                        AND PROD.APMCOD = PE.APMCOD
			                        AND ISNULL(PROD.UNDCOD02, -1) = ISNULL(PE.UNDCOD02, -1) 
			                        AND CASE WHEN PE.CPMQTDUND02 IS NOT NULL AND PE.CPMQTDUND02 > 0 
					                        THEN PE.CPMQTDUND / PE.CPMQTDUND02 
					                        ELSE PE.CPMQTDUND 
				                        END = 
				                        CASE WHEN PROD.CPMQTDUND02 IS NOT NULL AND PROD.CPMQTDUND02 > 0 
					                        THEN PROD.CPMQTDUND / PROD.CPMQTDUND02 
					                        ELSE PROD.CPMQTDUND 
				                        END 
			                        AND CASE WHEN PE.UNDCOD02 IS NOT NULL 
					                        THEN 
						                        CASE WHEN PE.CPMQTDUND02 IS NOT NULL AND PE.CPMQTDUND02 > 0 
							                        THEN PE.CPMQTDUND02 / PE.CPMQTDUND02 
							                        ELSE PE.CPMQTDUND02 
						                        END 
					                        ELSE -1 
				                        END = 
				                        CASE WHEN PROD.UNDCOD02 IS NOT NULL 
					                        THEN 
						                        CASE WHEN PROD.CPMQTDUND02 IS NOT NULL AND PROD.CPMQTDUND02 > 0 
							                        THEN PROD.CPMQTDUND02 / PROD.CPMQTDUND02 
							                        ELSE PROD.CPMQTDUND02 
						                        END 
					                        ELSE -1 
				                        END
	                        WHERE 
		                        PROD.PRME_CD_PRODUTO <> PE.PRME_CD_PRODUTO
		                        AND (PROD.PRMI_QT_APRESENTACAO = PE.PRMI_QT_APRESENTACAO OR PROD.PCMIDCCOMPAPRES = 0)
		                        AND (PROD.PCMIDCPSICOTROPICO = 0 OR 
			                        (
				                        CASE 
					                        WHEN PROD.PRMI_FL_CLASSIFICACAO ='R' AND (PE.PRMI_FL_CLASSIFICACAO IN('R','G') OR (PE.PRMI_FL_CLASSIFICACAO='S' AND PE.PCMIDCINTERCAMBIAVEL=1) ) THEN 1
					                        WHEN PROD.PRMI_FL_CLASSIFICACAO='G' AND PE.PRMI_FL_CLASSIFICACAO IN('R','G','S') THEN 1
					                        WHEN PROD.PRMI_FL_CLASSIFICACAO='S' THEN 0 
					                        ELSE 0 
				                        END
			                        ) = 1
		                        )	
	                        GROUP BY 
		                        PE.PRME_CD_PRODUTO
	                        HAVING 
		                        SUM (CASE WHEN PROD.CPMCOD IS NULL THEN -1 ELSE 1 END) =
		                        (
			                        SELECT COUNT(*)
			                        FROM BLOTBLPCMPRODCOMPOSICAO_TMP (NOLOCK) 
			                        WHERE PRME_CD_PRODUTO =  @Cod
		                        )
                        ) AS PRODUTOSEQUIVALENTES
                        WHERE (
	                        SELECT Count(*) 
	                        FROM BLOTBLPCMPRODCOMPOSICAO_TMP (NOLOCK) 
	                        WHERE PRME_CD_PRODUTO = PRODUTOSEQUIVALENTES.PRME_CD_PRODUTO
                        ) = (
	                        SELECT Count(*) 
	                        FROM BLOTBLPCMPRODCOMPOSICAO_TMP (NOLOCK) 
	                        WHERE PRME_CD_PRODUTO = @Cod
                        )
                        ORDER BY PRODUTOSEQUIVALENTES.PRME_CD_PRODUTO
                ";
            }
        }

        public static string CRITERIO_POR_EQUIVALENTES
        {
            get
            {
                return
                    @"AND PM.PRME_CD_PRODUTO in @Produtos
                            				) AS Produtos ON 1 = 1";
            }
        }

        public static string OBTER_PRODUTOS_EQUIVALENTES
        {
            get
            {
                return QUERY_PADRAO + CRITERIO_POR_EQUIVALENTES;
            }
        }

    }
}