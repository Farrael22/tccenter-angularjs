using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccenter.api.DataAccess.Repository.Login
{
    public interface ILoginRepository
    {
        IEnumerable<int> ValidarInformacoesLogin(string email, string senha);
    }
}