using System;

namespace tccenter.api.Helpers.Exceptions
{
    public class DataAtualizacaoException : Exception
    {
        public DataAtualizacaoException(string descricao) : base(descricao) { }
    }
}
