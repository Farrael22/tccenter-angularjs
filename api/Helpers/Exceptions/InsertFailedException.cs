using System;

namespace tccenter.api.Helpers.Exceptions
{
    public class InsertFailedException : Exception
    {
        public InsertFailedException(string descricao) : base(descricao) { }
    }
}
