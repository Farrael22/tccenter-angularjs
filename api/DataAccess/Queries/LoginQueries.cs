namespace tccenter.api.DataAccess.Queries
{
    public class LoginQueries
    {
        public static string VALIDAR_INFORMACOES_LOGIN
        {
            get
            {
                return @"
                    SELECT 
                    	IdUsuario
                    FROM USUARIO
                    WHERE EmailUsuario = @Email 
                    AND SenhaUsuario = @Senha
            ";
            }
        }
    }
}