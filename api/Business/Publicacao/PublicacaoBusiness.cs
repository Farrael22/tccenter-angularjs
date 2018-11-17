using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using tccenter.api.DataAccess.Repository.Curtida;
using tccenter.api.DataAccess.Repository.Mensagem;
using tccenter.api.DataAccess.Repository.Orientador;
using tccenter.api.DataAccess.Repository.Publicacao;
using tccenter.api.DataAccess.Repository.TopicosInteressantes;
using tccenter.api.DataAccess.Repository.Usuario;
using tccenter.api.Domain.DTO;
using tccenter.api.Domain.Entity;

namespace tccenter.api.Business.Publicacao
{
    public class PublicacaoBusiness : IPublicacaoBusiness
    {
        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly IOrientadorRepository _orientadorRepository;
        private readonly ITopicosInteressantesRepository _topicoRepository;
        private readonly ICurtidaRepository _curtidaRepository;
        private readonly IMensagemRepository _mensagemRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PublicacaoBusiness(IPublicacaoRepository publicacaoRepository, IOrientadorRepository orientadorRepository, ITopicosInteressantesRepository topicoRepository, ICurtidaRepository curtidaRepository, IMensagemRepository mensagemRepository, IUsuarioRepository usuarioRepository)
        {
            _publicacaoRepository = publicacaoRepository;
            _orientadorRepository = orientadorRepository;
            _topicoRepository = topicoRepository;
            _curtidaRepository = curtidaRepository;
            _mensagemRepository = mensagemRepository;
            _usuarioRepository = usuarioRepository;
        }

        public List<MensagemDTO> BuscarComentariosPublicacao(int idPublicacao)
        {
            var mensagens = Mapper.Map<List<MensagemDTO>>(_mensagemRepository.BuscarComentariosPublicacao(idPublicacao));

            foreach (var item2 in mensagens)
            {
                item2.Usuario = Mapper.Map<UsuarioDTO>(_usuarioRepository.BuscarInformacoesUsuario(item2.IdUsuarioComentou).First());
            }

            return mensagens;
        }

        public int CadastrarPublicacao(PublicacaoDTO publicacao)
        {
            publicacao.DataPublicacao = DateTime.Now;

            var entity = Mapper.Map<PublicacaoEntity>(publicacao);

            return _publicacaoRepository.CadastrarPublicacao(entity);
        }

        public int ComentarPublicacao(MensagemDTO mensagem)
        {
            var entity = Mapper.Map<MensagemEntity>(mensagem);

            return _mensagemRepository.ComentarPublicacao(entity);
        }

        public int CurtirPublicacao(int idUsuario, int idPublicacao)
        {
            return _curtidaRepository.CurtirPublicacao(idUsuario, idPublicacao);
        }

        public bool DescurtirPublicacao(int idUsuario, int idPublicacao)
        {
            _curtidaRepository.DescurtirPublicacao(idUsuario, idPublicacao);

            return true;
        }

        public List<PublicacaoDTO> ObterPublicacoesPorInteresseUsuario(int idUsuario)
        {
            var publicacoes = Mapper.Map<IEnumerable<PublicacaoDTO>>(_publicacaoRepository.ObterPublicacoesPorInteresseUsuario(idUsuario)).ToList();

            foreach (var item in publicacoes)
            {
                item.Orientador = Mapper.Map<OrientadorDTO>(_orientadorRepository.ObterOrientadorPorPublicacao(item.IdPublicacao).First());
                item.TopicoInteresse = Mapper.Map<TopicosInteressantesDTO>(_topicoRepository.ObterTopicosInteressantesPorPublicacao(item.IdPublicacao).First());
            }

            return publicacoes;
        }

        public int VerificarPublicacaoCurtida(int idUsuario, int idPublicacao)
        {
            return _curtidaRepository.VerificarPublicacaoCurtida(idUsuario, idPublicacao);
        }
    }
}