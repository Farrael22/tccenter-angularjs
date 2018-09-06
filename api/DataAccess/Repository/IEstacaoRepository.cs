using balcao.offline.api.Entity;
using System;
using System.Collections.Generic;

namespace balcao.offline.api.DataAccess.Repository
{
    public interface IEstacaoRepository
    {
        IEnumerable<EstacaoEntity> EstacaoPorIP(string ip);

        string PingFilialOnline();
    }
}
