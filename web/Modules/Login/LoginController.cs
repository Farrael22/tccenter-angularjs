﻿using Microsoft.Win32.SafeHandles;
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
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            var baseModel = new Modules.BaseModel();

            // Sinaliza pro front-end que precisa limpar ou nao o storage
            if (!string.IsNullOrEmpty(Request.QueryString["LimparLocalStorage"]))
                baseModel.LimparLocalStorage = Request.QueryString["LimparLocalStorage"];

            return View(baseModel);
        }

        public ActionResult Login()
        {
            return PartialView();
        }
    }
}