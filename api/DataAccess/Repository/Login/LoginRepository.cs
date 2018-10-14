using System.Collections.Generic;
using System.Configuration;
using tccenter.api.DataAccess.Queries;
using tccenter.api.Helpers.DataAccess;

namespace tccenter.api.DataAccess.Repository.Login
{
    public class LoginRepository : ILoginRepository
    {
        public IEnumerable<int> ValidarInformacoesLogin(string email, string senha)
        {
            using (var transaction = new TransactionHelperBaseGerente())
            {
                return transaction.Query<int>(LoginQueries.VALIDAR_INFORMACOES_LOGIN,
                    new { Email = email, Senha = senha });
            }
        }
    }
}