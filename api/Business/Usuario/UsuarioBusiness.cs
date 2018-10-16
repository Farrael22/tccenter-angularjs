using AutoMapper;
using System.Linq;
using tccenter.api.DataAccess.Repository.Usuario;
using tccenter.api.Domain.DTO;
using tccenter.api.Domain.Entity;
using tccenter.api.Helpers.Exceptions;

namespace tccenter.api.Business.Usuario
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioBusiness(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public int CadastrarUsuario(UsuarioDTO infoUsuario)
        {
            if (infoUsuario == null)
                throw new BadRequestException("As informações do usuário não foram enviadas para a API");

            var usuarioEntity = Mapper.Map<UsuarioEntity>(infoUsuario);

            var retorno = _usuarioRepository.CadastarUsuario(usuarioEntity);
            
            return retorno;
        }
    }
}