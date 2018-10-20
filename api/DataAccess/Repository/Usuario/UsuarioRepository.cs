using System.Collections.Generic;
using tccenter.api.DataAccess.Queries;
using tccenter.api.Domain.Entity;
using tccenter.api.Helpers.DataAccess;

namespace tccenter.api.DataAccess.Repository.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public IEnumerable<int> ValidarInformacoesLogin(string email, string senha)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<int>(UsuarioQueries.VALIDAR_INFORMACOES_LOGIN,
                    new { Email = email, Senha = senha });
            }
        }
        public int CadastarUsuario(UsuarioEntity usuario)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.ExecuteScalar<int>(UsuarioQueries.CADASTRAR_USUARIO,
                    new { Nome = usuario.Nome, Avatar = usuario.Avatar, Senha = usuario.Senha, Profissao = usuario.Profissao, Email = usuario.Email });
            }
        }

        public IEnumerable<UsuarioEntity> BuscarInformacoesUsuario(int idUsuario)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<UsuarioEntity>(UsuarioQueries.BUSCAR_INFORMACOES_USUARIO,
                    new { IdUsuario = idUsuario });
            }
        }
    }
}