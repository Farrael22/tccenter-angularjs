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
    [RoutePrefix("pesquisa")]
    public class ProdutosController : BaseApiController
    {
        // GET: Fornecedor
        private readonly IProdutosBusiness _camadaBusiness;
        public ProdutosController(IProdutosBusiness camadaBusiness)
        {
            _camadaBusiness = camadaBusiness;
        }

        /// <summary>
        /// Pesquisa de produtos por Filial
        /// </summary>
        /// <param name="filial">Id da Filial que está pesquisando</param>
        /// <param name="texto">Palavra chave. Pode conter Codigo Interno, Codigo de Barras, Nome, Descricao ou Princio Ativo</param>       
        /// <response code="200">Ok</response>   
        /// <response code="404">Não encontrado</response>
        /// <response code="406">Parâmetros de pesquisa inválidos</response>
        /// <response code="412">Base desatualizada</response>
        /// <response code="503">Base indisponível</response>
        /// <response code="500">Erro inesperado</response>
        //[LogFilter(DisableTrace = true)]
        //[CacheOutput(ClientTimeSpan = 600, ServerTimeSpan = 600)]
        [AllowAnonymous]
        [Route("filial/{filial:int}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "ObterPesquisaProdutos", Type = typeof(ResultList<ProdutoDTO>))]
        [SwaggerResponse(HttpStatusCode.NotAcceptable, Description = "Parâmetros de entrada inválido", Type = typeof(RetornoErro))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Erro não tratado", Type = typeof(void))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, Description = "Base desatualizada", Type = typeof(RetornoErro))]
        [SwaggerResponse(HttpStatusCode.ServiceUnavailable, Description = "Banco de dados indisponível", Type = typeof(ResultList<EstacaoDTO>))]
        public IHttpActionResult Filial(int filial, [FromUri] string texto)
        {
            var result = new ResultList<ProdutoDTO>();
            result.Data = _camadaBusiness.Busca(filial, texto);

            if (result.Data != null)
                return Encontrado(result);

            throw new NotFoundException(MessageHelper.PRODUTOS_NAO_ENCONTRADOS);
        }

        /// <summary>
        /// Obtem lista de produtos equivalentes
        /// </summary>            
        /// <response code="200">Ok</response>        
        /// <response code="404">Não encontrado</response>
        /// <response code="406">Parâmetros de pesquisa inválidos</response>
        /// <response code="412">Base desatualizada</response>
        /// <response code="503">Base indisponível</response>
        /// <response code="500">Erro inesperado</response>
        [AllowAnonymous]
        [Route("filial/{filial:int}/equivalentes")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "ObterProdutosEquivalentes", Type = typeof(ResultList<ProdutoDTO>))]
        [SwaggerResponse(HttpStatusCode.NotAcceptable, Description = "Parâmetros de entrada inválidos", Type = typeof(RetornoErro))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Erro não tratado", Type = typeof(void))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Não encontrado", Type = typeof(RetornoErro))]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, Description = "Base desatualizada", Type = typeof(RetornoErro))]
        [SwaggerResponse(HttpStatusCode.ServiceUnavailable, Description = "Banco de dados indisponível", Type = typeof(ResultList<EstacaoDTO>))]
        public IHttpActionResult Equivalentes(int filial, [FromUri] int codigoProduto)
        {
            var result = new ResultList<ProdutoDTO>();
            result.Data = _camadaBusiness.Equivalentes(filial, codigoProduto);
            if (result.Data != null)
                return Encontrado(result);

            throw new NotFoundException(MessageHelper.PRODUTOS_EQUIVALENTES_NAO_ENCONTRADOS);
        }
    }
}