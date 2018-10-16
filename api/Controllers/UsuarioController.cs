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
        [Route("cadastrar", Name = "cadastrar")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Cadastro realizado com sucesso", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult EfetuarLogin([FromBody] UsuarioDTO infoUsuario)
        {
           var result = _camadaBusiness.CadastrarUsuario(infoUsuario);

            return Encontrado(result);
        }
    }
}