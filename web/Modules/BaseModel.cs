using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Modules
{
    // Model comum para todas views
    public class BaseModel
    {
        public string WebUrl
        {
            get
            {
                return string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, HttpRuntime.AppDomainAppVirtualPath);
            }
        }

        public string ApiUrl
        {
            get
            {
                return Config.AppSettings.API_URL;
            }
        }

        public string Version
        {
            get
            {
                return this.GetType().Assembly.GetName().Version.ToString();
            }
        }

        public string _IpEstacao;
        public string IpEstacao
        {
            get { return string.IsNullOrEmpty(_IpEstacao) ? HttpContext.Current.Request.UserHostAddress : _IpEstacao; }
            set { _IpEstacao = value; }
        }

        public string _LimparLocalStorage="false";
        public string LimparLocalStorage
        {
            get { return _LimparLocalStorage; }
            set { _LimparLocalStorage = value; }
        }
    }
}