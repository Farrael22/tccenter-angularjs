using System;

namespace tccenter.api.Helpers.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string descricao) : base(descricao) { }
    }
}
