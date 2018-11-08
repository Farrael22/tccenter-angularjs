using System.Collections.Generic;
using tccenter.api.DataAccess.Queries;
using tccenter.api.Domain.Entity;
using tccenter.api.Helpers.DataAccess;

namespace tccenter.api.DataAccess.Repository.Publicacao
{
    public class PublicacaoRepository : IPublicacaoRepository
    {
        public IEnumerable<PublicacaoEntity> BuscarPublicacaoPorUsuario(int idUsuario)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<PublicacaoEntity>(PublicacaoQueries.BUSCAR_PUBLICACAO_POR_USUARIO,
                    new { IdUsuario = idUsuario });
            }
        }
    }
}