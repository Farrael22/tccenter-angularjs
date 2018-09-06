using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using balcao.offline.api.Business;
using balcao.offline.api.DTO;
using balcao.offline.api.Helpers.Exceptions;
using balcao.offline.api.Helpers.Controllers;
using Swashbuckle.Swagger.Annotations;
using System.Data.SqlClient;
using balcao.offline.api.Helpers;

namespace balcao.offline.api.Controllers
{

    /// <summary>
    /// Organiza as rotas da API para o modulo de Estacao
    /// </summary>
    [Authorize]
    [RoutePrefix("estacao")]
    public class EstacaoController : BaseApiController
    {
        // GET: Estacoes
        private readonly IEstacaoBusiness _camadaBusiness;
        public EstacaoController(IEstacaoBusiness camadaBusiness)
        {
            _camadaBusiness = camadaBusiness;
        }

        /// <summary>
        /// Retorna as estações vinculadas a um IP
        /// </summary>
        /// <param name="endereco">Endereço IP da estação</param>
        /// <response code="200">Ok</response>        
        /// <response code="404">Não encontrado</response>
        /// <response code="406">Parâmetros de pesquisa inválidos</response>
        /// <response code="412">Base desatualizada</response>
        /// <response code="503">Base indisponível</response>
        /// <response code="500">Erro inesperado</response>
        [AllowAnonymous]
        [Route("ip", Name = "ObterEstacaoPorIP")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "ObterEstacaoIP", Type = typeof(ResultList<EstacaoDTO>))]
        [SwaggerResponse(HttpStatusCode.NotAcceptable, Description = "Parâmetros de entrada inválidos", Type = typeof(RetornoErro))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Erro não tratado", Type = typeof(void))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, Description = "Base desatualizada", Type = typeof(RetornoErro))]
        [SwaggerResponse(HttpStatusCode.ServiceUnavailable, Description = "Banco de dados indisponível", Type = typeof(ResultList<EstacaoDTO>))]
        public IHttpActionResult ObterEstacaoIP([FromUri] string endereco)
        {
            var result = new ResultList<EstacaoDTO>();
            result.Data = _camadaBusiness.ObterEstacaoPorIP(endereco);

            if (result.Data != null)
                return Encontrado(result);

            throw new NotFoundException(MessageHelper.ESTACAO_NAO_ENCONTRADA);
        }

        /// <summary>
        /// Retorna as estações vinculadas a um IP
        /// </summary>
        /// <response code="200">Ok</response>        
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Erro inesperado</response>
        [AllowAnonymous]
        [Route("ping", Name = "PingFilialOnline")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "PingFilialOnline", Type = typeof(ResultList<EstacaoDTO>))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Não encontrado", Type = typeof(RetornoErro))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Erro não tratado", Type = typeof(void))]
        public IHttpActionResult PingFilialOnline()
        {
            var result = _camadaBusiness.PingFilialOnline();

            if (result != null)
                return Encontrado(result);

            throw new NotFoundException(MessageHelper.API_ONLINE_INDISPONIVELL);
        }
    }
}