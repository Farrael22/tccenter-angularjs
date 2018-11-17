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

        public int CadastrarPublicacao(PublicacaoEntity publicacao)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.ExecuteScalar<int>(PublicacaoQueries.CADASTRAR_PUBLICACAO,
                    new
                    {
                        IdUsuario = publicacao.IdUsuario,
                        IdTopicosInteressantes = publicacao.TopicoInteresse.IdTopicosInteressantes,
                        IdOrientador = publicacao.Orientador.IdOrientador,
                        TituloPublicacao = publicacao.TituloPublicacao,
                        LinkPublicacao = publicacao.LinkPublicacao,
                        DescPublicacao = publicacao.DescPublicacao,
                        DataPublicacao = publicacao.DataPublicacao,
                        ResultadoPublicacao = publicacao.ResultadoPublicacao
                    });
            }
        }

        public IEnumerable<PublicacaoEntity> ObterPublicacoesPorInteresseUsuario(int idUsuario)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<PublicacaoEntity>(PublicacaoQueries.OBTER_PUBLICACOES_POR_INTERESSE_USUARIO,
                    new
                    {
                        IdUsuario = idUsuario
                    });
            }
        }
    }
}