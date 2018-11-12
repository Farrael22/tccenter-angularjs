using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using tccenter.api.Business.Usuario;
using tccenter.api.Domain.DTO;
using tccenter.api.Helpers.Controllers;

namespace tccenter.api.Controllers
{
    [RoutePrefix("usuario")]
    public class UsuarioController : BaseApiController
    {
        private readonly IUsuarioBusiness _camadaBusiness;
        public UsuarioController(IUsuarioBusiness camadaBusiness)
        {
            _camadaBusiness = camadaBusiness;
        }
        

        [AllowAnonymous]
        [Route("cadastrar", Name = "cadastrar")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Cadastro realizado com sucesso", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult CadastrarUsuario([FromBody] UsuarioDTO infoUsuario)
        {
           var result = _camadaBusiness.CadastrarUsuario(infoUsuario);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("alterar", Name = "alterar")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Cadastro realizado com sucesso", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult AlterarUsuario([FromBody] UsuarioDTO infoUsuario)
        {
            var result = _camadaBusiness.AlterarUsuario(infoUsuario);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("login", Name = "login")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "ObterListagemKitsVirtuais", Type = typeof(UsuarioDTO))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult EfetuarLogin([FromBody] LoginDTO infoLogin)
        {
            var result = _camadaBusiness.EfetuarLogin(infoLogin);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("buscarPorId", Name = "buscarPorId")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "ObterListagemKitsVirtuais", Type = typeof(UsuarioDTO))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult BuscarPorId([FromUri] int idUsuario)
        {
            var result = _camadaBusiness.BuscarPorId(idUsuario);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("quantidadePublicacao", Name = "quantidadePublicacao")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "ObterListagemKitsVirtuais", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult BuscarQuantidadePublicacao([FromUri] int idUsuario)
        {
            var result = _camadaBusiness.BuscarQuantidadePublicacao(idUsuario);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("quantidadeSeguidores", Name = "quantidadeSeguidores")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "ObterListagemKitsVirtuais", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult BuscarQuantidadeSeguidores([FromUri] int idUsuario)
        {
            var result = _camadaBusiness.BuscarQuantidadeSeguidores(idUsuario);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("usuariosSeguidos", Name = "usuariosSeguidos")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "ObterListagemKitsVirtuais", Type = typeof(List<UsuarioDTO>))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult BuscarUsuariosSeguidos([FromUri] int idUsuario)
        {
            var result = _camadaBusiness.BuscarUsuariosSeguidos(idUsuario);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("seguirUsuario", Name = "seguirUsuario")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "ObterListagemKitsVirtuais", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult SeguirUsuario([FromUri] int idUsuarioLogado, [FromUri] int idSeguir)
        {
            var result = _camadaBusiness.SeguirUsuario(idUsuarioLogado, idSeguir);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("pararSeguirUsuario", Name = "pararSeguirUsuario")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "ObterListagemKitsVirtuais", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult PararSeguirUsuario([FromUri] int idUsuarioLogado, [FromUri] int idPararSeguir)
        {
            _camadaBusiness.PararSeguirUsuario(idUsuarioLogado, idPararSeguir);

            return Encontrado("OK");
        }
    }
}