using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccenter.api.DataAccess.Queries
{
    public class InteressesUsuariosQueries
    {
        public static string CADASTRAR_INTERESSE_USUARIO
        {
            get
            {
                return @"
                    INSERT INTO InteresseUsuario
                    VALUES (@IdUsuario, @IdTopico)

                    SELECT SCOPE_IDENTITY()
            ";
            }
        }
    }
}