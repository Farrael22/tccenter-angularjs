using System.Collections.Generic;
using tccenter.api.Domain.Entity;

namespace tccenter.api.DataAccess.Repository.Orientador
{
    public interface IOrientadorRepository
    {
        IEnumerable<OrientadorEntity> ObterOrientadores();
        IEnumerable<OrientadorEntity> ObterOrientadorPorPublicacao(int idOrientador);
    }
}