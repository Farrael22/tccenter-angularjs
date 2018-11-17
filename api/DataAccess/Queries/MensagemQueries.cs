namespace tccenter.api.DataAccess.Queries
{
    public class MensagemQueries
    {
        public static string BUSCAR_COMENTARIOS_PUBLICACAO
        {
            get
            {
                return @"
                    SELECT
                    	IdMensagem,
                    	IdPublicacao,
                    	IdUsuarioComentou,
                    	DescMensagem,
                    	DataMensagem
                    FROM Mensagem
                    WHERE IdPublicacao = @IdPublicacao
                    ORDER BY DataMensagem";
            }
        }

        public static string COMENTAR_PUBLICACAO
        {
            get
            {
                return @"
                    INSERT INTO Mensagem
                    VALUES (@IdPublicacao, @IdUsuario, @DescMensagem, @DataMensagem)
                    
                    SELECT SCOPE_IDENTITY();";
            }
        }
    }
}