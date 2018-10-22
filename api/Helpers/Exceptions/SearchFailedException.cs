using System;

namespace tccenter.api.Helpers.Exceptions
{
    public class SearchFailedException : Exception
    {
        public SearchFailedException(string descricao) : base(descricao) { }
    }
}