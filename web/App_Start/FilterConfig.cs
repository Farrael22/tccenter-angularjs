using System.Web;
using System.Web.Mvc;
using Bugsnag.Clients;

namespace web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(WebMVCClient.ErrorHandler());
        }
    }
}