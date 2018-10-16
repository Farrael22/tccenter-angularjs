using System.Collections.Generic;
using tccenter.api.Domain.Entity;

namespace tccenter.api.DataAccess.Repository.Usuario
{
    public interface IUsuarioRepository
    {
        int CadastarUsuario(UsuarioEntity usuario);
    }
}