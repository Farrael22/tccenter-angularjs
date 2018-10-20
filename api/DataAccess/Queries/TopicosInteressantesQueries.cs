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
            ";
            }
        }

        public static string OBTER_TOPICOS_INTERESSANTES_POR_USUARIO
        {
            get
            {
                return @"
                    SELECT
	                	topicos.IdTopicosInteressantes as IdTopicosInteressantes,
	                	topicos.DescTopico as DescricaoTopico
	                FROM InteresseUsuario interesses
	                INNER JOIN TopicosInteressantes topicos
	                	ON interesses.IdTopicosInteressantes = topicos.IdTopicosInteressantes
	                WHERE interesses.IdUsuario = @IdUsuario

            ";
            }
        }
    }
}