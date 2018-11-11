using AutoMapper;
using System;
using tccenter.api.DataAccess.Repository.Publicacao;
using tccenter.api.Domain.DTO;
using tccenter.api.Domain.Entity;

namespace tccenter.api.Business.Publicacao
{
    public class PublicacaoBusiness : IPublicacaoBusiness
    {
        private readonly IPublicacaoRepository _publicacaoRepository;

        public PublicacaoBusiness(IPublicacaoRepository publicacaoRepository)
        {
            _publicacaoRepository = publicacaoRepository;
        }

        public int CadastrarPublicacao(PublicacaoDTO publicacao)
        {
            publicacao.DataPublicacao = DateTime.Now;

            var entity = Mapper.Map<PublicacaoEntity>(publicacao);

            return _publicacaoRepository.CadastrarPublicacao(entity);
        }
    }
}