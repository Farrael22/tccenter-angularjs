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
        public IHttpActionResult CadastrarPublicacao([FromBody] PublicacaoDTO infoPublicacao)
        {
            var result = _camadaBusiness.CadastrarPublicacao(infoPublicacao);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("obterPublicacaointeresse", Name = "obterPublicacaointeresse")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Cadastro realizado com sucesso", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult ObterPublicacoesPorInteresseUsuario([FromUri] int idUsuario)
        {
            var result = _camadaBusiness.ObterPublicacoesPorInteresseUsuario(idUsuario);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("verificarCurtida", Name = "verificarCurtida")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Cadastro realizado com sucesso", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult VerificarPublicacaoCurtida([FromUri] int idUsuario, [FromUri] int idPublicacao)
        {
            var result = _camadaBusiness.VerificarPublicacaoCurtida(idUsuario, idPublicacao);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("curtirPublicacao", Name = "curtirPublicacao")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Cadastro realizado com sucesso", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult CurtirPublicacao([FromUri] int idUsuario, [FromUri] int idPublicacao)
        {
            var result = _camadaBusiness.CurtirPublicacao(idUsuario, idPublicacao);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("descurtirPublicacao", Name = "descurtirPublicacao")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Cadastro realizado com sucesso", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult DescurtirPublicacao([FromUri] int idUsuario, [FromUri] int idPublicacao)
        {
            var result = _camadaBusiness.DescurtirPublicacao(idUsuario, idPublicacao);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("comentarPublicacao", Name = "comentarPublicacao")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Cadastro realizado com sucesso", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult ComentarPublicacao([FromBody] MensagemDTO mensagem)
        {
            var result = _camadaBusiness.ComentarPublicacao(mensagem);

            return Encontrado(result);
        }

        [AllowAnonymous]
        [Route("obterComentarios", Name = "obterComentarios")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Cadastro realizado com sucesso", Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = " Não encontrado", Type = typeof(RetornoErro))]
        public IHttpActionResult BuscarComentariosPublicacao([FromUri] int idPublicacao)
        {
            var result = _camadaBusiness.BuscarComentariosPublicacao(idPublicacao);

            return Encontrado(result);
        }
    }
}