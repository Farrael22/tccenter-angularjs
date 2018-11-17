namespace tccenter.api.DataAccess.Queries
{
    public class CurtidaQueries
    {
        public static string VERIFICAR_PUBLICACAO_CURTIDA
        {
            get
            {
                return @"
                    SELECT 
                    	IdPublicacao 
                    FROM Curtida
                    WHERE IdUsuarioCurtiu = @IdUsuario
                    AND IdPublicacao = @IdPublicacao";
            }
        }

        public static string CURTIR_PUBLICACAO
        {
            get
            {
                return @"
                    INSERT INTO Curtida
                    VALUES (@IdPublicacao, @IdUsuario, @DataCurtida)

                    SELECT SCOPE_IDENTITY();";
            }
        }

        public static string DESCURTIR_PUBLICACAO
        {
            get
            {
                return @"
                    DELETE Curtida
                    WHERE IdPublicacao = @IdPublicacao
                    AND IdUsuarioCurtiu = @IdUsuario";
            }
        }
    }
}