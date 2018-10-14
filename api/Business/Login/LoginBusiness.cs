using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tccenter.api.DataAccess.Repository.Login;
using tccenter.api.Domain.DTO;
using tccenter.api.Domain.Entity;
using tccenter.api.Helpers.Exceptions;

namespace tccenter.api.Business.Login
{
    public class LoginBusiness : ILoginBusiness
    {
        private readonly ILoginRepository _loginRepository;
        public LoginBusiness(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public int EfetuarLogin(LoginDTO infoLogin)
        {
            LoginEntity loginEntity = null;

            if (string.IsNullOrWhiteSpace(infoLogin.Email) || string.IsNullOrWhiteSpace(infoLogin.Senha))
                throw new BadRequestException("A lista de produtos deve ser enviada");

            var retorno = _loginRepository.ValidarInformacoesLogin(infoLogin.Email, infoLogin.Senha);

            if(retorno.Count() == 0)
                throw new NotFoundException("Usuário não encontrado");

            return retorno.First();
        }
    }
}