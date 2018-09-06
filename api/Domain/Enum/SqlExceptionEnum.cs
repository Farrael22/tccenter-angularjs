using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace balcao.offline.api.Domain.Enum
{
    public enum SqlExceptionEnum
    {
        [Description("A connection was successfully established with the server, but then an error occurred during the login process")]
        ErrorLoginException = 233,

        [Description("Access to selected database has been denied")]
        AccessDeniedException = 4060,

        [Description("Fatal error during the query execution")]
        FatalErrorException = 0,

        [Description("Fatal error during the query execution class")]
        FatalErrorClassException = 11

    }
}