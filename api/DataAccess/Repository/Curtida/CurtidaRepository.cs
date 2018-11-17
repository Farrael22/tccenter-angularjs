using System;
using tccenter.api.DataAccess.Queries;
using tccenter.api.Helpers.DataAccess;

namespace tccenter.api.DataAccess.Repository.Curtida
{
    public class CurtidaRepository : ICurtidaRepository
    {
        public int CurtirPublicacao(int idUsuario, int idPublicacao)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.ExecuteScalar<int>(CurtidaQueries.CURTIR_PUBLICACAO,
                    new { IdUsuario = idUsuario, IdPublicacao = idPublicacao, DataCurtida = DateTime.Now });
            }
        }

        public void DescurtirPublicacao(int idUsuario, int idPublicacao)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                transaction.Execute(CurtidaQueries.DESCURTIR_PUBLICACAO,
                    new { IdUsuario = idUsuario, IdPublicacao = idPublicacao });
            }
        }

        public int VerificarPublicacaoCurtida(int idUsuario, int idPublicacao)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.ExecuteScalar<int>(CurtidaQueries.VERIFICAR_PUBLICACAO_CURTIDA,
                    new { IdUsuario = idUsuario, IdPublicacao = idPublicacao });
            }
        }
    }
}