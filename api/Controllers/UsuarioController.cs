using Swashbuckle.Swagger.Annotations;
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
        public IHttpActionResult EfetuarLogin([FromBody] UsuarioDTO infoUsuario)
        {
           var result = _camadaBusiness.CadastrarUsuario(infoUsuario);

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
    }
}