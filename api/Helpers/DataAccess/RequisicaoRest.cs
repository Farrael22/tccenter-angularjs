using RestSharp;

namespace tccenter.api.Helpers.DataAccess
{
    public class RequisicaoRest : RestRequest
    {
        public RequisicaoRest(string recurso) : base(recurso)
        {

        }
        public void AdicionarParametro(string nome, object valor)
        {
            AddParameter(nome, valor);
        }
    }
}
