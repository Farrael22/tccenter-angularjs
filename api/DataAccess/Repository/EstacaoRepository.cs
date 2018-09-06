using balcao.offline.api.Entity;
using balcao.offline.api.DataAccess.Queries;
using System.Collections.Generic;
using System.Configuration;
using balcao.offline.api.Helpers.DataAccess;

namespace balcao.offline.api.DataAccess.Repository
{
    public class EstacaoRepository : ClienteApiBase, IEstacaoRepository
    {
        public EstacaoRepository() : base(ConfigurationManager.AppSettings.Get("UrlApiFiliais"))
        { }

        public IEnumerable<EstacaoEntity> EstacaoPorIP(string ip)
        {
            using (var transaction = new TransactionHelperBaseGerente())
            {
                return transaction.Query<EstacaoEntity>(EstacaoQueries.OBTER_ESTACAO_POR_IP,
                    new { IP = ip });
            }
        }

        public string PingFilialOnline()
        {
            var requisicao = new RequisicaoRest("ping");
            requisicao.Method = RestSharp.Method.GET;

            var retornoApi = Ping(requisicao);

            return retornoApi;
        }
    }
}