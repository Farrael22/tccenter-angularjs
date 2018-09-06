using System.Web.Http;
using balcao.offline.api.Helpers.Business;
using IoC;

namespace balcao.offline.api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.RegisterMappings();
            InjetorConfig.RegistrarContainer();
        }
    }
}
