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
    }
}