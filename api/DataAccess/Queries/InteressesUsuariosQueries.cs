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
                    VALUES (@IdUsuario, @IdTopicoMestre, @IdTopicoInteresse)

                    SELECT SCOPE_IDENTITY()
            ";
            }
        }

        public static string DELETAR_INTERESSES_USUARIO
        {
            get
            {
                return @"
                    DELETE InteresseUsuario
                    WHERE IdUsuario = @IdUsuario
            ";
            }
        }
    }
}