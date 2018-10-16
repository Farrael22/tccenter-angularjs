using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccenter.api.DataAccess.Queries
{
    public class UsuarioQueries
    {
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

                    SELECT SCOPE_IDENTITY()
            ";
            }
        }
    }
}