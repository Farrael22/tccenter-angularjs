using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccenter.api.DataAccess.Repository.InteressesUsuarios
{
    public interface IInteressesUsuarios
    {
        int CadastrarTopicoInteressante(int idUsuario, int idTopico);
    }
}