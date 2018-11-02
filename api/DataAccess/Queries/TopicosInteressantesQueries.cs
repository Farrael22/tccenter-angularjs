namespace tccenter.api.DataAccess.Queries
{
    public class TopicosInteressantesQueries
    {
        public static string OBTER_TOPICOS_INTERESSANTES
        {
            get
            {
                return @"
                    SELECT 
                    	IdTopicosInteressantes,
                        DescTopico as DescricaoTopico
                    FROM TOPICOSINTERESSANTES
                    WHERE IdTopicoMestre = @IdTopicoMestre
            ";
            }
        }

        public static string OBTER_TOPICOS_INTERESSANTES_POR_USUARIO
        {
            get
            {
                return @"
                    SELECT
	                	mestre.IdTopicoMestre,
						topicos.IdTopicosInteressantes,
	                	topicos.DescTopico as DescricaoTopico
	                FROM InteresseUsuario interesses
	                INNER JOIN TopicosInteressantes topicos
	                	ON interesses.IdTopicosInteressantes = topicos.IdTopicosInteressantes
					INNER JOIN TopicoMestre mestre
						ON mestre.IdTopicoMestre = topicos.IdTopicoMestre
	                WHERE interesses.IdUsuario = @IdUsuario

            ";
            }
        }

        public static string OBTER_TOPICOS_MESTRE_POR_USUARIO
        {
            get
            {
                return @"
                    SELECT DISTINCT
	                	mestre.IdTopicoMestre,
						mestre.DescricaoTopicoMestre
	                FROM InteresseUsuario interesses
	                INNER JOIN TopicosInteressantes topicos
	                	ON interesses.IdTopicosInteressantes = topicos.IdTopicosInteressantes
					INNER JOIN TopicoMestre mestre
						ON mestre.IdTopicoMestre = topicos.IdTopicoMestre
	                WHERE interesses.IdUsuario = @IdUsuario

            ";
            }
        }
    }
}