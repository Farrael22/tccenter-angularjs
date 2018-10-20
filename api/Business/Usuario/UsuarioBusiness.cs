using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using tccenter.api.DataAccess.Repository.InteressesUsuarios;
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

        public UsuarioBusiness(IUsuarioRepository usuarioRepository, IInteressesUsuarios interesseUsuarioRepositoy, ITopicosInteressantesRepository topicosRepository)
        {
            _usuarioRepository = usuarioRepository;
            _interesseUsuarioRepositoy = interesseUsuarioRepositoy;
            _topicosRepository = topicosRepository;
        }

        public UsuarioDTO EfetuarLogin(LoginDTO infoLogin)
        {
            if (string.IsNullOrWhiteSpace(infoLogin.Email) || string.IsNullOrWhiteSpace(infoLogin.Senha))
                throw new BadRequestException("A lista de produtos deve ser enviada");

            var idUsuarioCadastrado = _usuarioRepository.ValidarInformacoesLogin(infoLogin.Email, infoLogin.Senha);

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

            foreach (var item in infoUsuario.TopicosInteressantes)
            {
                if(string.IsNullOrWhiteSpace(item.IdTopicosInteressantes))
                    throw new BadRequestException("Não foi possível realizar o cadastro dos Topicos de Interesse");

                _interesseUsuarioRepositoy.CadastrarTopicoInteressante(idUsuarioCadastrado, Convert.ToInt32(item.IdTopicosInteressantes));
            }
            
            return idUsuarioCadastrado;
        }

        private UsuarioDTO BuscarInformacoesUsuario(int idUsuario)
        {
            var usuarioEntity = _usuarioRepository.BuscarInformacoesUsuario(idUsuario);
            var usuarioDTO = Mapper.Map<UsuarioDTO>(usuarioEntity.First());

            var listaTopicosEntity = _topicosRepository.ObterTopicosInteressantesPorUsuario(idUsuario);
            usuarioDTO.TopicosInteressantes = Mapper.Map<List<TopicosInteressantesDTO>>(listaTopicosEntity);

            return usuarioDTO;
        }
    }
}