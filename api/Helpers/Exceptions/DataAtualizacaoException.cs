using System;

namespace balcao.offline.api.Helpers.Exceptions
{
    public class DataAtualizacaoException : Exception
    {
        public DataAtualizacaoException(string descricao) : base(descricao) { }
    }
}
