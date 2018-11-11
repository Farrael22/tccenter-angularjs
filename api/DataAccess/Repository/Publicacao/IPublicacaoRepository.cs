using System.Collections.Generic;
using tccenter.api.Domain.Entity;

namespace tccenter.api.DataAccess.Repository.Publicacao
{
    public interface IPublicacaoRepository
    {
        IEnumerable<PublicacaoEntity> BuscarPublicacaoPorUsuario(int idUsuario);
        int CadastrarPublicacao(PublicacaoEntity publicacao);
    }
}