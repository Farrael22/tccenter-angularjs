using Swashbuckle.Application;
using System.Web.Http;

namespace balcao.offline.api
{
    public static class SwaggerConfig
    {
        public static void Register(string version, string title, string xmlCommentsPath)
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion(version, title);
                    c.IncludeXmlComments(GetXmlCommentsPath(xmlCommentsPath));
                })
                .EnableSwaggerUi(c =>
                {

                });
        }

        static string GetXmlCommentsPath(string xmlCommentsPath)
        {
            return string.Format(xmlCommentsPath, System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}