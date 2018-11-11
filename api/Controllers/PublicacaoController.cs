using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using tccenter.api.Business.Publicacao;
using tccenter.api.Domain.DTO;
using tccenter.api.Helpers.Controllers;

namespace tccenter.api.Controllers
{
    [RoutePrefix("publicacao")]
    public class PublicacaoController : BaseApiController
    {
        private readonly IPublicacaoBusiness _camadaBusiness;
        public PublicacaoController(IPublicacaoBusiness camadaBusiness)
        {
            _camadaBusiness = camadaBusiness;
        }


        [AllowAnonymous]
        [Route("cadastrarPublicacao", Name = "cadastrarPublicacao")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Cadastro realizado com sucesso", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult CadastrarUsuario([FromBody] PublicacaoDTO infoPublicacao)
        {
            var result = _camadaBusiness.CadastrarPublicacao(infoPublicacao);

            return Encontrado(result);
        }
    }
}