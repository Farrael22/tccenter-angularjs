using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using tccenter.api.DataAccess.Repository.InteressesUsuarios;
using tccenter.api.DataAccess.Repository.Publicacao;
using tccenter.api.DataAccess.Repository.TopicosInteressantes;
using tccenter.api.DataAccess.Repository.Usuario;
using tccenter.api.Domain.DTO;
using tccenter.api.Domain.Entity;
using tccenter.api.Helpers;
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

            var senhaCriptografada = CriptografiaSenha.Crypt(infoLogin.Senha);

            var idUsuarioCadastrado = _usuarioRepository.ValidarInformacoesLogin(infoLogin.Email, senhaCriptografada);

            if (idUsuarioCadastrado.Count() == 0)
                throw new BadRequestException("As informações de login estão inválidas");

            var usuarioDTO = this.BuscarInformacoesUsuario(idUsuarioCadastrado.First());

            if (usuarioDTO == null)
                throw new NotFoundException("Usuario não encontrado");

            return usuarioDTO;
        }

        public int AlterarUsuario(UsuarioDTO infoUsuario)
        {
            if (!string.IsNullOrWhiteSpace(infoUsuario.Senha) && !string.IsNullOrWhiteSpace(infoUsuario.SenhaAntiga))
            {
                var senhaAntigaCriptografada = CriptografiaSenha.Crypt(infoUsuario.SenhaAntiga);
                var idUsuarioValido = _usuarioRepository.ValidarSenha(infoUsuario.Id, senhaAntigaCriptografada);

                if (idUsuarioValido != infoUsuario.Id)
                    throw new BadRequestException("A senha antiga informada está incorreta.");

                var senhaNovaCriptografada = CriptografiaSenha.Crypt(infoUsuario.Senha);
                _usuarioRepository.AlterarSenhaUsuario(infoUsuario.Id, senhaNovaCriptografada);
            }

            var usuarioEntity = Mapper.Map<UsuarioEntity>(infoUsuario);

            _usuarioRepository.AlterarUsuario(usuarioEntity);
            _interesseUsuarioRepositoy.DeletarInteressesUsuario(infoUsuario.Id);

            foreach (var item in infoUsuario.TopicosInteressesMestre)
            {
                foreach (var topico in item.TopicosInteressantes)
                {
                    _interesseUsuarioRepositoy.CadastrarTopicoInteressante(infoUsuario.Id, item.IdTopicoMestre, topico.IdTopicosInteressantes);
                }
            }

            return infoUsuario.Id;
        }

        public int CadastrarUsuario(UsuarioDTO infoUsuario)
        {
            infoUsuario.Senha = CriptografiaSenha.Crypt(infoUsuario.Senha);

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
                {
                    IdTopicosInteressantes = item.IdTopicosInteressantes,
                    DescricaoTopico = item.DescricaoTopico
                });
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

        public UsuarioDTO BuscarPorId(int idUsuario)
        {
            var usuarioDTO = this.BuscarInformacoesUsuario(idUsuario);

            if (usuarioDTO == null)
                throw new NotFoundException("Usuario não encontrado");

            return usuarioDTO;
        }

        public int SeguirUsuario(int idUsuarioLogado, int idSeguir)
        {
            if(idUsuarioLogado == 0 || idSeguir == 0)
                throw new BadRequestException("Impossível seguir usuário com as informações fornecidas.");

            return _usuarioRepository.SeguirUsuario(idUsuarioLogado, idSeguir);
        }

        public void PararSeguirUsuario(int idUsuarioLogado, int idPararSeguir)
        {
            if (idUsuarioLogado == 0 || idPararSeguir == 0)
                throw new BadRequestException("Impossível parar de seguir usuário com as informações fornecidas.");

            _usuarioRepository.PararSeguirUsuario(idUsuarioLogado, idPararSeguir);
        }

        public List<UsuarioDTO> ObterSugestaoUsuarios(int idUsuario)
        {
            return Mapper.Map<List<UsuarioDTO>>(_usuarioRepository.ObterSugestaoUsuarios(idUsuario));
        }
    }
}