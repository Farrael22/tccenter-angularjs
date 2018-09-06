using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace balcao.offline.api.Helpers.DataAccess
{
    public class ClienteApiBase
    {
        private readonly RestClient _clienteApi;
        public ClienteApiBase(string url)
        {
            _clienteApi = new RestClient(url);
        }      

        protected string Ping(RequisicaoRest requisicao)
        {
            var resposta = _clienteApi.Get(requisicao);
            return resposta.ResponseStatus == ResponseStatus.Completed ? resposta.Content : null;
        }
    }
}
