using AutoMapper;
using System;
using System.Linq;
using tccenter.api.DataAccess.Repository.InteressesUsuarios;
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

        public UsuarioBusiness(IUsuarioRepository usuarioRepository, IInteressesUsuarios interesseUsuarioRepositoy)
        {
            _usuarioRepository = usuarioRepository;
            _interesseUsuarioRepositoy = interesseUsuarioRepositoy;
        }

        public int CadastrarUsuario(UsuarioDTO infoUsuario)
        {
            if (infoUsuario == null)
                throw new BadRequestException("As informações do usuário não foram enviadas para a API");

            var usuarioEntity = Mapper.Map<UsuarioEntity>(infoUsuario);

            var idUsuarioCadastrado = _usuarioRepository.CadastarUsuario(usuarioEntity);

            foreach (var item in infoUsuario.InteressesUsuario)
            {
                if(string.IsNullOrWhiteSpace(item.IdTopicos))
                    throw new BadRequestException("Não foi possível realizar o cadastro dos Topicos de Interesse");

                _interesseUsuarioRepositoy.CadastrarTopicoInteressante(idUsuarioCadastrado, Convert.ToInt32(item.IdTopicos));
            }
            
            return idUsuarioCadastrado;
        }
    }
}