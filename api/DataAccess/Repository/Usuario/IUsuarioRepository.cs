using System.Collections.Generic;
using tccenter.api.Domain.Entity;

namespace tccenter.api.DataAccess.Repository.Usuario
{
    public interface IUsuarioRepository
    {
        IEnumerable<int> ValidarInformacoesLogin(string email, string senha);
        int CadastarUsuario(UsuarioEntity usuario);
        void AlterarUsuario(UsuarioEntity usuario);
        IEnumerable<UsuarioEntity> BuscarInformacoesUsuario(int idUsuario);
        int BuscarQuantidadePublicacao(int idUsuario);
        int BuscarQuantidadeSeguidores(int idUsuario);
        IEnumerable<UsuarioEntity> BuscarUsuariosSeguidos(int idUsuario);
        int ValidarSenha(int idUsuario, string senha);
        void AlterarSenhaUsuario(int idUsuario, string senha);
        int SeguirUsuario(int idUsuarioLogado, int idSeguir);
        void PararSeguirUsuario(int idUsuarioLogado, int idPararSeguir);
    }
}