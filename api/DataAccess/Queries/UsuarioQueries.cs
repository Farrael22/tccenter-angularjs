using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccenter.api.DataAccess.Queries
{
    public class UsuarioQueries
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

        public static string CADASTRAR_USUARIO
        {
            get
            {
                return @"
                        INSERT INTO 
                            USUARIO
                        VALUES(
                            @Nome, 
                            @Avatar, 
                            @Email, 
                            @Senha, 
                            @Profissao);

                        SELECT SCOPE_IDENTITY()";
            }
        }

        public static string BUSCAR_INFORMACOES_USUARIO
        {
            get
            {
                return @"
                        SELECT
                        	usu.IdUsuario as Id,
                        	NomeUsuario as Nome,
                        	AvatarUsuario as Avatar,
                        	EmailUsuario as Email,
                        	ProfissaoUsuario as Profissao
                        FROM Usuario usu                        
                        WHERE usu.IdUsuario = @IdUsuario";
            }
        }
    }
}