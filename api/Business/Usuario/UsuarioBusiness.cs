using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using tccenter.api.DataAccess.Repository.InteressesUsuarios;
using tccenter.api.DataAccess.Repository.Publicacao;
using tccenter.api.DataAccess.Repository.TopicosInteressantes;
using tccenter.api.DataAccess.Repository.Usuario;
using tccenter.api.Domain.DTO;
using tccenter.api.Domain.Entity;
using tccenter.api.Helpers.Exceptions;

namespace tccenter.api.Business.Usuario
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IInteressesUsuarios _interesseUsuarioRepositoy;
        private readonly ITopicosInteressantesRepository _topicosRepository;
        private readonly IPublicacaoRepository _publicacaoRepository;

        public UsuarioBusiness(IUsuarioRepository usuarioRepository, IInteressesUsuarios interesseUsuarioRepositoy, ITopicosInteressantesRepository topicosRepository, IPublicacaoRepository publicacaoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _interesseUsuarioRepositoy = interesseUsuarioRepositoy;
            _topicosRepository = topicosRepository;
            _publicacaoRepository = publicacaoRepository;
        }

        public UsuarioDTO EfetuarLogin(LoginDTO infoLogin)
        {
            if (string.IsNullOrWhiteSpace(infoLogin.Email) || string.IsNullOrWhiteSpace(infoLogin.Senha))
                throw new BadRequestException("A informação de Email e Senha devem ser enviados");

            var idUsuarioCadastrado = _usuarioRepository.ValidarInformacoesLogin(infoLogin.Email, infoLogin.Senha);

            if (idUsuarioCadastrado.Count() == 0)
                throw new BadRequestException("As informações de login estão inválidas");

            var usuarioDTO = this.BuscarInformacoesUsuario(idUsuarioCadastrado.First());

            if (usuarioDTO == null)
                throw new NotFoundException("Usuario não encontrado");

            return usuarioDTO;
        }

        public int CadastrarUsuario(UsuarioDTO infoUsuario)
        {
            if (infoUsuario == null)
                throw new BadRequestException("As informações do usuário não foram enviadas para a API");

            var usuarioEntity = Mapper.Map<UsuarioEntity>(infoUsuario);

            var idUsuarioCadastrado = _usuarioRepository.CadastarUsuario(usuarioEntity);

            foreach (var item in infoUsuario.TopicosInteressesMestre)
            {
                foreach (var topico in item.TopicosInteressantes)
                {
                    if (topico.IdTopicosInteressantes == 0)
                        throw new BadRequestException("Não foi possível realizar o cadastro dos Topicos de Interesse");

                    _interesseUsuarioRepositoy.CadastrarTopicoInteressante(idUsuarioCadastrado, item.IdTopicoMestre, topico.IdTopicosInteressantes);
                }
            }

            return idUsuarioCadastrado;
        }

        private UsuarioDTO BuscarInformacoesUsuario(int idUsuario)
        {
            var usuarioEntity = _usuarioRepository.BuscarInformacoesUsuario(idUsuario);
            var usuarioDTO = Mapper.Map<UsuarioDTO>(usuarioEntity.First());

            var listaTopicoMestre = Mapper.Map<List<TopicoMestreDTO>>(_topicosRepository.ObterTopicosMestrePorUsuario(idUsuario));
            var listaTopicosInteresse = _topicosRepository.ObterTopicosInteressantesPorUsuario(idUsuario);

            foreach (var item in listaTopicosInteresse)
            {
                var topicoMestre = listaTopicoMestre.Where(x => x.IdTopicoMestre == item.IdTopicoMestre).FirstOrDefault();

                if (topicoMestre.TopicosInteressantes == null)
                    topicoMestre.TopicosInteressantes = new List<TopicosInteressantesDTO>();

                topicoMestre.TopicosInteressantes.Add(new TopicosInteressantesDTO()
                                                        { IdTopicosInteressantes = item.IdTopicosInteressantes,
                                                          DescricaoTopico = item.DescricaoTopico });
            }
            usuarioDTO.TopicosInteressesMestre = listaTopicoMestre;

            return usuarioDTO;
        }

        public int BuscarQuantidadePublicacao(int idUsuario)
        {
            return _usuarioRepository.BuscarQuantidadePublicacao(idUsuario);
        }

        public int BuscarQuantidadeSeguidores(int idUsuario)
        {
            return _usuarioRepository.BuscarQuantidadeSeguidores(idUsuario);
        }

        public List<UsuarioDTO> BuscarUsuariosSeguidos(int idUsuario)
        {
            var listaUsuarios = Mapper.Map<List<UsuarioDTO>>(_usuarioRepository.BuscarUsuariosSeguidos(idUsuario));
            return listaUsuarios.ToList();
        }
    }
}