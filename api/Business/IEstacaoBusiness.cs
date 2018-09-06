using System.Collections.Generic;
using balcao.offline.api.DTO;

namespace balcao.offline.api.Business
{
    public interface IEstacaoBusiness
    {
        IEnumerable<EstacaoDTO> ObterEstacaoPorIP(string ip);

        string PingFilialOnline();
    }
}