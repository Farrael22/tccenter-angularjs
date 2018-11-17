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

        public static string OBTER_PUBLICACOES_POR_INTERESSE_USUARIO
        {
            get
            {
                return @"
                    SELECT TOP 20
                    	IdPublicacao,
                    	pub.IdUsuario,
                    	TituloPublicacao,
                    	LinkPublicacao,
                    	DescPublicacao,
                    	DataPublicacao,
                    	ResultadoPublicacao
                    FROM Publicacao pub
                    INNER JOIN InteresseUsuario intusu
                    ON pub.IdTopicosInteressantes = intusu.IdTopicosInteressantes
                    AND intusu.IdUsuario = @IdUsuario
                    ORDER BY DataPublicacao DESC";
            }
        }
    }
}