using System;

namespace balcao.offline.api.Helpers.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string mensagem) : base(mensagem) { }
    }
}
