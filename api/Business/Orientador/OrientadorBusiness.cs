using AutoMapper;
using System.Collections.Generic;
using tccenter.api.DataAccess.Repository.Orientador;
using tccenter.api.Domain.DTO;

namespace tccenter.api.Business.Orientador
{
    public class OrientadorBusiness : IOrientadorBusiness
    {
        private readonly IOrientadorRepository _orientadorRepository;

        public OrientadorBusiness(IOrientadorRepository orientadorRepository)
        {
            _orientadorRepository = orientadorRepository;
        }

        public IEnumerable<OrientadorDTO> ObterOrientadores()
        {
            return Mapper.Map<IEnumerable<OrientadorDTO>>(_orientadorRepository.ObterOrientadores());
        }
    }
}