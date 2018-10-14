using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;
using tccenter.api.Business.Login;
using tccenter.api.Domain.DTO;
using tccenter.api.Helpers.Controllers;

namespace tccenter.api.Controllers
{
    [RoutePrefix("login")]
    public class LoginController : BaseApiController
    {
        private readonly ILoginBusiness _camadaBusiness;
        public LoginController(ILoginBusiness camadaBusiness)
        {
            _camadaBusiness = camadaBusiness;
        }

        /// <summary>
        /// Pesquisa os kits da lista de produtos informada
        /// </summary>
        /// <param name="listaProdutos">Lista de produtos a serem analisados</param>
        /// <response code="200">Ok</response>        
        /// <response code="404">Não encontrado</response>
        /// <response code="406">Parâmetros de pesquisa inválidos</response>
        /// <response code="412">Base desatualizada</response>
        /// <response code="503">Base indisponível</response>
        /// <response code="500">Erro inesperado</response>
        [AllowAnonymous]
        [Route("efetuarLogin", Name = "efetuarLogin")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "ObterListagemKitsVirtuais", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult EfetuarLogin([FromBody] LoginDTO infoLogin)
        {
            var result = _camadaBusiness.EfetuarLogin(infoLogin);

            return Encontrado(result);
        }
    }
}