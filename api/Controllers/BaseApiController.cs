using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace balcao.offline.api.Controllers
{
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dadosRetorno"></param>
        /// <returns></returns>
        protected IHttpActionResult Encontrado<TRetorno>(TRetorno dadosRetorno)
        {         
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, dadosRetorno));
        }

        /// <summary>
        /// 201
        /// </summary>
        /// <param name="dadosRetorno"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        protected IHttpActionResult Criado(object dadosRetorno/*, Uri location*/)
        {
            var resposta = Request.CreateResponse(HttpStatusCode.Created, dadosRetorno);
            //resposta.Headers.Location = location;
            return ResponseMessage(resposta);
        }

        /// <summary>
        /// 204
        /// </summary>
        /// <param name="dadosRetorno"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        protected IHttpActionResult Atualizado(object dadosRetorno, Uri location)
        {
            var resposta = Request.CreateResponse(HttpStatusCode.OK, dadosRetorno);
            resposta.Headers.Location = location;
            return ResponseMessage(resposta);
        }

        /// <summary>
        /// 204
        /// </summary>
        /// <returns></returns>
        protected IHttpActionResult Excluido()
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}