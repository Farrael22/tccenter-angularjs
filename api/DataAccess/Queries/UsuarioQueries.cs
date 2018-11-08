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

        public static string BUSCAR_QUANTIDADE_PUBLICACAO
        {
            get
            {
                return @"
                        SELECT 
                            count(IdUsuario) 
                        FROM Publicacao
                        where IdUsuario = @IdUsuario";
            }
        }

        public static string BUSCAR_QUANTIDADE_SEGUIDORES
        {
            get
            {
                return @"
                        SELECT 
                            count(IdUsuarioSeguir) 
                        FROM UsuarioSeguido
                        where IdUsuarioSeguir = @IdUsuario";
            }
        }

        public static string BUSCAR_USUARIOS_SEGUIDOS
        {
            get
            {
                return @"
                        SELECT 
                        	u.IdUsuario as Id,
                        	u.NomeUsuario as Nome,
                        	AvatarUsuario as Avatar
                        FROM UsuarioSeguido us
                        	INNER JOIN Usuario u
                        	ON us.IdUsuarioSeguir = u.IdUsuario
                        WHERE IdUsuarioSeguidor = @IdUsuario";
            }
        }
    }
}