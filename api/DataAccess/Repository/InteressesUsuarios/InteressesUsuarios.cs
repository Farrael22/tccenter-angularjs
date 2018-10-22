using tccenter.api.DataAccess.Queries;
using tccenter.api.Helpers.DataAccess;

namespace tccenter.api.DataAccess.Repository.InteressesUsuarios
{
    public class InteressesUsuarios : IInteressesUsuarios
    {
        public int CadastrarTopicoInteressante(int idUsuario, int idTopicoMestre, int idTopicoInteresse)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.ExecuteScalar<int>(InteressesUsuariosQueries.CADASTRAR_INTERESSE_USUARIO,
                    new { IdUsuario = idUsuario, IdTopicoMestre = idTopicoMestre, IdTopicoInteresse = idTopicoInteresse });
            }
        }
    }
}