namespace tccenter.api.DataAccess.Queries
{
    public class OrientadorQueries
    {
        public static string BUSCAR_ORIENTADORES
        {
            get
            {
                return @"
                    SELECT
                        IdOrientador,
	                    NomeOrientador,
	                    ContatoOrientador
                    FROM Orientador";
            }
        }

        public static string OBTER_ORIENTADOR_POR_PUBLICACAO
        {
            get
            {
                return @"
                    SELECT 
                    	ori.*
                    FROM Orientador ori
                    INNER JOIN Publicacao pub
                    ON pub.IdOrientador = ori.IdOrientador";
            }
        }
    }
}