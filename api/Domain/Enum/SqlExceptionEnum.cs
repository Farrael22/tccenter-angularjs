using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace tccenter.api.Domain.Enum
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
        FatalErrorClassException = 11,

        [Description("Unique Key violation class")]
        UniqueKeyClassException = 14,

        [Description("Unique Key violation number")]
        UniqueKeyNumberException = 2627

    }
}