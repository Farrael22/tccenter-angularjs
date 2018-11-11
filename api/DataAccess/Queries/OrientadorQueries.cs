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
    }
}