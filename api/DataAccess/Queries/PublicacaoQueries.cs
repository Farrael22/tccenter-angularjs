namespace tccenter.api.DataAccess.Queries
{
    public class PublicacaoQueries
    {
        public static string BUSCAR_PUBLICACAO_POR_USUARIO
        {
            get
            {
                return @"
                    SELECT 
                    	IdUsuario,
                    	IdPublicacao,
                    	TitutloPublicacao,
                    	LinkPublicacao,
                    	DescPublicacao,
                    	DataPublicacao,
                    	ResultadoPublicacao
                    FROM Publicacao
                    where IdUsuario = @IdUsuario";
            }
        }

        public static string CADASTRAR_PUBLICACAO
        {
            get
            {
                return @"
                    INSERT INTO Publicacao
                    VALUES(
                    	@IdUsuario,
                    	@IdTopicosInteressantes,
                    	@IdOrientador,
                    	@TituloPublicacao,
                    	@LinkPublicacao,
                    	@DescPublicacao,
                    	@DataPublicacao,
                    	@ResultadoPublicacao
                    );

                    SELECT SCOPE_IDENTITY();";
            }
        }
    }
}