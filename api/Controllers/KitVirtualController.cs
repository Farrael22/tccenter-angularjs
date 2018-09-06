using balcao.offline.api.Business;
using balcao.offline.api.DTO;
using balcao.offline.api.Helpers;
using balcao.offline.api.Helpers.Controllers;
using balcao.offline.api.Helpers.Exceptions;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace balcao.offline.api.Controllers
{
    [RoutePrefix("kitvirtual")]
    public class KitVirtualController : BaseApiController
    {
        private readonly IKitVirtualBusiness _camadaBusiness;
        public KitVirtualController(IKitVirtualBusiness camadaBusiness)
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
        [Route("ObterKitsVirtuaisPossiveis", Name = "ObterKitsVirtuaisPossiveis")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "ObterListagemKitsVirtuais", Type = typeof(ResultList<KitVirtualDTO.KitVirtualRetorno>))]
        [SwaggerResponse(HttpStatusCode.NotAcceptable, Description = "Parâmetros de entrada inválido", Type = typeof(RetornoErro))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Erro não tratado", Type = typeof(void))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, Description = "Base desatualizada", Type = typeof(RetornoErro))]
        [SwaggerResponse(HttpStatusCode.ServiceUnavailable, Description = "Banco de dados indisponível", Type = typeof(RetornoErro))]
        public IHttpActionResult ObterKitsVirtuaisPossiveis([FromBody]  List<KitVirtualDTO.Produto> listaProdutos)
        {
            if (listaProdutos == null || listaProdutos.Count <= 0)
                throw new BadRequestException("A lista de produtos deve ser enviada");

            var result = new ResultList<KitVirtualDTO.KitVirtualRetorno>();
            result.Data = _camadaBusiness.ObterKitsVirtuais(listaProdutos);

            if (result.Data != null)
                return Encontrado(result);

            throw new NotFoundException(MessageHelper.KITS_NAO_ENCONTRADOS);
        }
    }
}