using System.Collections.Generic;
using tccenter.api.Domain.DTO;

namespace tccenter.api.Business.Publicacao
{
    public interface IPublicacaoBusiness
    {
        int CadastrarPublicacao(PublicacaoDTO publicacao);
        List<PublicacaoDTO> ObterPublicacoesPorInteresseUsuario(int idUsuario);
        int VerificarPublicacaoCurtida(int idUsuario, int idPublicacao);
        bool DescurtirPublicacao(int idUsuario, int idPublicacao);
        int CurtirPublicacao(int idUsuario, int idPublicacao);
        int ComentarPublicacao(MensagemDTO mensagem);
        List<MensagemDTO> BuscarComentariosPublicacao(int idPublicacao);
    }
}