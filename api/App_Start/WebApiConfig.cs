using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http.Headers;
using Swashbuckle.Application;
using tccenter.api.Helpers.Exceptions;

namespace tccenter.api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            RegisterPrivate(config, "v1", "API TCCenter", @"{0}\bin\tccenter.api.XML");
        }

        private static void RegisterPrivate(HttpConfiguration config, string version, string title, string xmlCommentsPath)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "swagger_root",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler((message) => message.RequestUri.ToString(), "swagger/ui/index")
            );

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Filters.Add(new GlobalExceptionAttribute());

            SwaggerConfig.Register(version, title, xmlCommentsPath);
        }
    }
}
