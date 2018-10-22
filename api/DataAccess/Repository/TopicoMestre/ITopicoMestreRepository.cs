using System.Collections.Generic;
using tccenter.api.Domain.Entity;

namespace tccenter.api.DataAccess.Repository.TopicoMestre
{
    public interface ITopicoMestreRepository
    {
        IEnumerable<TopicoMestreEntity> ObterTopicosMestre();
    }
}