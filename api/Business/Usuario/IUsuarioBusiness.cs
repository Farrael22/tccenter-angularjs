using System.Collections.Generic;
using tccenter.api.Domain.DTO;

namespace tccenter.api.Business.Usuario
{
    public interface IUsuarioBusiness
    {
        UsuarioDTO EfetuarLogin(LoginDTO infoLogin);
        int CadastrarUsuario(UsuarioDTO infoUsuario);
        int AlterarUsuario(UsuarioDTO infoUsuario);
        int BuscarQuantidadePublicacao(int idUsuario);
        int BuscarQuantidadeSeguidores(int idUsuario);
        List<UsuarioDTO> BuscarUsuariosSeguidos(int idUsuario);
        UsuarioDTO BuscarPorId(int idUsuario);
        int SeguirUsuario(int idUsuarioLogado, int idSeguir);
        void PararSeguirUsuario(int idUsuarioLogado, int idPararSeguir);
    }
}