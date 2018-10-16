using tccenter.api.Domain.DTO;

namespace tccenter.api.Business.Usuario
{
    public interface IUsuarioBusiness
    {
        int CadastrarUsuario(UsuarioDTO infoUsuario);
    }
}