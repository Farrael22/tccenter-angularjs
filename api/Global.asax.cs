using System.Web.Http;
using tccenter.api.Helpers.Business;
using IoC;

namespace tccenter.api
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
