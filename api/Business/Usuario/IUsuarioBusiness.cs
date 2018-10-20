using tccenter.api.Domain.DTO;

namespace tccenter.api.Business.Usuario
{
    public interface IUsuarioBusiness
    {
        UsuarioDTO EfetuarLogin(LoginDTO infoLogin);
        int CadastrarUsuario(UsuarioDTO infoUsuario);
    }
}