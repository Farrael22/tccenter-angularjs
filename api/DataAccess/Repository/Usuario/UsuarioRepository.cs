using System.Collections.Generic;
using tccenter.api.DataAccess.Queries;
using tccenter.api.Domain.Entity;
using tccenter.api.Helpers.DataAccess;

namespace tccenter.api.DataAccess.Repository.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public int CadastarUsuario(UsuarioEntity usuario)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.ExecuteScalar<int>(UsuarioQueries.CADASTRAR_USUARIO,
                    new { Nome = usuario.Nome, Avatar = usuario.Avatar, Senha = usuario.Senha, Profissao = usuario.Profissao, Email = usuario.Email });
            }
        }
    }
}