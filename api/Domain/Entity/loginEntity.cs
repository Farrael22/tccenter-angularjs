using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccenter.api.Domain.Entity
{
    public class LoginEntity
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? IdUsuario { get; set; }
    }
}