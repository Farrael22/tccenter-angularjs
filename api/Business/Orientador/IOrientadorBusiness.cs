using System.Collections.Generic;
using tccenter.api.Domain.DTO;

namespace tccenter.api.Business.Orientador
{
    public interface IOrientadorBusiness
    {
        IEnumerable<OrientadorDTO> ObterOrientadores();
    }
}