using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using tccenter.api.Domain.Enum;
using tccenter.api.Helpers.Controllers;
//using NLog;

namespace tccenter.api.Helpers.Exceptions
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

            if (actionExecutedContext.Exception is InsertFailedException)
                throw new HttpResponseException(actionExecutedContext.Request.CreateResponse(HttpStatusCode.PreconditionFailed, new RetornoErro { ErroDeNegocio = true, Mensagem = actionExecutedContext.Exception.Message }));
        
            if (actionExecutedContext.Exception is SqlException)
            {
                var exception = actionExecutedContext.Exception as SqlException;

                if(exception.Number == (int)SqlExceptionEnum.UniqueKeyNumberException && exception.Class == (byte)SqlExceptionEnum.UniqueKeyClassException)
                    throw new HttpResponseException(actionExecutedContext.Request.CreateResponse(HttpStatusCode.Conflict, new RetornoErro { ErroDeNegocio = true, Mensagem = "Já existe usuário cadastrado para o E-mail informado." }));
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