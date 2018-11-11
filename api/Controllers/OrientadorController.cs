using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using tccenter.api.Business.Orientador;
using tccenter.api.Domain.DTO;
using tccenter.api.Helpers.Controllers;

namespace tccenter.api.Controllers
{
    [RoutePrefix("orientador")]
    public class OrientadorController : BaseApiController
    {
        private readonly IOrientadorBusiness _camadaBusiness;
        public OrientadorController(IOrientadorBusiness camadaBusiness)
        {
            _camadaBusiness = camadaBusiness;
        }


        [AllowAnonymous]
        [Route("obterOrientadores", Name = "obterOrientadores")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Cadastro realizado com sucesso", Type = typeof(List<OrientadorDTO>))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult ObterOrientadores()
        {
            var result = _camadaBusiness.ObterOrientadores();

            return Encontrado(result);
        }
    }
}