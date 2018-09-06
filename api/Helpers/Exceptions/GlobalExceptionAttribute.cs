using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using balcao.offline.api.Domain.Enum;
using balcao.offline.api.Helpers.Controllers;
//using NLog;

namespace balcao.offline.api.Helpers.Exceptions
{
    public class GlobalExceptionAttribute : ExceptionFilterAttribute
    {
        //static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is BadRequestException)
                throw new HttpResponseException(actionExecutedContext.Request.CreateResponse(HttpStatusCode.NotAcceptable, new RetornoErro { ErroDeNegocio = true, Mensagem =  actionExecutedContext.Exception.Message }));

            if (actionExecutedContext.Exception is NotFoundException)
                throw new HttpResponseException(actionExecutedContext.Request.CreateResponse(HttpStatusCode.NotFound, new RetornoErro { ErroDeNegocio = true, Mensagem = actionExecutedContext.Exception.Message }));

            if (actionExecutedContext.Exception is DataAtualizacaoException)
                throw new HttpResponseException(actionExecutedContext.Request.CreateResponse(HttpStatusCode.PreconditionFailed, new RetornoErro { ErroDeNegocio = true, Mensagem = actionExecutedContext.Exception.Message }));
            
            if (actionExecutedContext.Exception is SqlException)
            {
                var exception = actionExecutedContext.Exception as SqlException;

                if(exception.Number == Convert.ToInt32(SqlExceptionEnum.AccessDeniedException) || exception.Number == Convert.ToInt32(SqlExceptionEnum.ErrorLoginException))
                    throw new HttpResponseException(actionExecutedContext.Request.CreateResponse(HttpStatusCode.ServiceUnavailable, new RetornoErro { ErroDeNegocio = true, Mensagem = MessageHelper.BANCO_INDISPONIVEL }));
            }

            //Logger.Error(new
            //{
            //    Requisicao = actionExecutedContext.Request,
            //    Erro = actionExecutedContext.Exception
            //});
            throw new HttpResponseException(actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new RetornoErro { ErroDeNegocio = false, Mensagem = actionExecutedContext.Exception.Message }));
        }
    }
}