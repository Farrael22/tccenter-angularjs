using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;
using tccenter.api.Business.TopicosInteressantes;
using tccenter.api.Domain.DTO;
using tccenter.api.Helpers.Controllers;

namespace tccenter.api.Controllers
{
    [RoutePrefix("topicosInteressantes")]
    public class TopicosInteressantesController : BaseApiController
    {
        private readonly ITopicosInteressantesBusiness _camadaBusiness;
        public TopicosInteressantesController(ITopicosInteressantesBusiness camadaBusiness)
        {
            _camadaBusiness = camadaBusiness;
        }

        /// <summary>
        /// Pesquisa os kits da lista de produtos informada
        /// </summary>
        /// <param name="">Lista de produtos a serem analisados</param>
        /// <response code="200">Ok</response>        
        /// <response code="404">Não encontrado</response>
        /// <response code="406">Parâmetros de pesquisa inválidos</response>
        /// <response code="412">Base desatualizada</response>
        /// <response code="503">Base indisponível</response>
        /// <response code="500">Erro inesperado</response>
        [AllowAnonymous]
        [Route("obter", Name = "obter")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "ObterTopicosInteressantes", Type = typeof(TopicosInteressantesDTO))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult ObterTopicosInteressantes()
        {
            var result = _camadaBusiness.ObterTopicosInteressantes();

            return Encontrado(result);
        }
    }
}