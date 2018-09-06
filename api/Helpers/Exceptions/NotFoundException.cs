using System;

namespace balcao.offline.api.Helpers.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string descricao) : base(descricao) { }
    }
}
