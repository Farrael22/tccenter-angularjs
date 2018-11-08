using System.Collections.Generic;
using tccenter.api.Domain.DTO;

namespace tccenter.api.Business.Usuario
{
    public interface IUsuarioBusiness
    {
        UsuarioDTO EfetuarLogin(LoginDTO infoLogin);
        int CadastrarUsuario(UsuarioDTO infoUsuario);
        int BuscarQuantidadePublicacao(int idUsuario);
        int BuscarQuantidadeSeguidores(int idUsuario);
        List<UsuarioDTO> BuscarUsuariosSeguidos(int idUsuario);
    }
}