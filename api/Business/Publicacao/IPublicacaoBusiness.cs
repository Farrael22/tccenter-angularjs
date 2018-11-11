using tccenter.api.Domain.DTO;

namespace tccenter.api.Business.Publicacao
{
    public interface IPublicacaoBusiness
    {
        int CadastrarPublicacao(PublicacaoDTO publicacao);
    }
}