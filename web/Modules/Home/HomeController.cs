using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace web.Modules.Home
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            return PartialView("Home");
        }

        public ActionResult ModalCadastroPublicacao()
        {
            return PartialView("ModalCadastroPublicacao/ModalCadastroPublicacao");
        }
    }
}