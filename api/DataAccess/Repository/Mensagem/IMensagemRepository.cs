using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tccenter.api.Domain.Entity;

namespace tccenter.api.DataAccess.Repository.Mensagem
{
    public interface IMensagemRepository
    {
        IEnumerable<MensagemEntity> BuscarComentariosPublicacao(int idPublicacao);
        int ComentarPublicacao(MensagemEntity mensagem);
    }
}