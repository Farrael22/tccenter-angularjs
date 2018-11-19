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

        public int ValidarSenha(int idUsuario, string senha)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.ExecuteScalar<int>(UsuarioQueries.VALIDAR_SENHA_USUARIO,
                    new { IdUsuario = idUsuario, Senha = senha });
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

        public int BuscarQuantidadePublicacao(int idUsuario)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.ExecuteScalar<int>(UsuarioQueries.BUSCAR_QUANTIDADE_PUBLICACAO,
                    new { IdUsuario = idUsuario });
            }
        }

        public int BuscarQuantidadeSeguidores(int idUsuario)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.ExecuteScalar<int>(UsuarioQueries.BUSCAR_QUANTIDADE_SEGUIDORES,
                    new { IdUsuario = idUsuario });
            }
        }

        public IEnumerable<UsuarioEntity> BuscarUsuariosSeguidos(int idUsuario)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<UsuarioEntity>(UsuarioQueries.BUSCAR_USUARIOS_SEGUIDOS,
                    new { IdUsuario = idUsuario });
            }
        }

        public void AlterarUsuario(UsuarioEntity usuario)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                transaction.Execute(UsuarioQueries.ALTERAR_USUARIO,
                    new { IdUsuario = usuario.Id, Nome = usuario.Nome, Profissao = usuario.Profissao });
            }
        }

        public void AlterarSenhaUsuario(int idUsuario, string senha)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                transaction.Execute(UsuarioQueries.ALTERAR_SENHA_USUARIO,
                    new { IdUsuario = idUsuario, Senha = senha });
            }
        }

        public int SeguirUsuario(int idUsuarioLogado, int idSeguir)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.ExecuteScalar<int>(UsuarioQueries.SEGUIR_USUARIO,
                    new { IdUsuarioSeguidor = idUsuarioLogado, IdUsuarioSeguir = idSeguir });
            }
        }

        public void PararSeguirUsuario(int idUsuarioLogado, int idPararSeguir)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                transaction.Execute(UsuarioQueries.PARAR_SEGUIR_USUARIO,
                    new { IdUsuarioSeguidor = idUsuarioLogado, IdPararSeguir = idPararSeguir });
            }
        }

        public IEnumerable<UsuarioEntity> ObterSugestaoUsuarios(int idUsuario)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<UsuarioEntity>(UsuarioQueries.OBTER_SUGESTAO_USUARIOS,
                    new { IdUsuario = idUsuario });
            }
        }
    }
}