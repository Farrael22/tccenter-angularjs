namespace tccenter.api.DataAccess.Queries
{
    public class TopicoMestreQueries
    {
        public static string OBTER_TOPICOS_MESTRE
        {
            get
            {
                return @"
                    SELECT 
                    	IdTopicoMestre,
                        DescricaoTopicoMestre
                    FROM TopicoMestre";
            }
        }
    }
}