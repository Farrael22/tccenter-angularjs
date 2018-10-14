using System;

namespace tccenter.api.Helpers.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string mensagem) : base(mensagem) { }
    }
}
