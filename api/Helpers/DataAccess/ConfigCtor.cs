using System.Data;

namespace tccenter.api.Helpers.DataAccess
{
    /// <summary>
    /// Parametros de configuracao comuns a todos Models. 
    /// Serao injetados ao abrir a transacao.
    /// </summary>
    public class ConfigCtor
    {
        private IDbConnection Connection;
        private IDbTransaction Transaction;

        public IDbConnection GetConnection()
        {
            return Connection;
        }

        public void SetConnection(IDbConnection connection)
        {
            Connection = connection;
        }

        public IDbTransaction GetTransaction()
        {
            return Transaction;
        }

        public void SetTransaction(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}