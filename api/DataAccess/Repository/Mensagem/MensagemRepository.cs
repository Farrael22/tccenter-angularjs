using System;
using System.Collections.Generic;
using tccenter.api.DataAccess.Queries;
using tccenter.api.Domain.Entity;
using tccenter.api.Helpers.DataAccess;

namespace tccenter.api.DataAccess.Repository.Mensagem
{
    public class MensagemRepository : IMensagemRepository
    {
        public IEnumerable<MensagemEntity> BuscarComentariosPublicacao(int idPublicacao)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.Query<MensagemEntity>(MensagemQueries.BUSCAR_COMENTARIOS_PUBLICACAO,
                    new
                    {
                        IdPublicacao = idPublicacao
                    });
            }
        }

        public int ComentarPublicacao(MensagemEntity mensagem)
        {
            using (var transaction = new TransactionHelperTccenter())
            {
                return transaction.ExecuteScalar<int>(MensagemQueries.COMENTAR_PUBLICACAO,
                    new
                    {
                        IdPublicacao = mensagem.IdPublicacao,
                        IdUsuario = mensagem.IdUsuarioComentou,
                        DescMensagem = mensagem.DescMensagem,
                        DataMensagem = DateTime.Now
                    });
            }
        }
    }
}