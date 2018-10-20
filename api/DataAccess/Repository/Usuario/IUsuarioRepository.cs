using System.Collections.Generic;
using tccenter.api.Domain.Entity;

namespace tccenter.api.DataAccess.Repository.Usuario
{
    public interface IUsuarioRepository
    {
        IEnumerable<int> ValidarInformacoesLogin(string email, string senha);
        int CadastarUsuario(UsuarioEntity usuario);
        IEnumerable<UsuarioEntity> BuscarInformacoesUsuario(int idUsuario);
    }
}