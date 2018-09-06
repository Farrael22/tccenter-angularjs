using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Modules
{
    public class BaseController : Controller
    {
        // TODO: COLOCAR AQUI TUDO QUE FOR COMUM PARA AS CONTROLLERS

        public ActionResult Shared(string modulo, string view)
        {
            return PartialView(modulo + "/" + view);
        }
    }
}