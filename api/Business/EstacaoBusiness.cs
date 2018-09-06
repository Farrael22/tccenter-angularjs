using System;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using balcao.offline.api.Helpers.Business;
using balcao.offline.api.DTO;
using balcao.offline.api.Entity;
using balcao.offline.api.DataAccess.Repository;
using balcao.offline.api.Helpers.Exceptions;

namespace balcao.offline.api.Business
{
    public class EstacaoBusiness : IEstacaoBusiness
    {
        private readonly IEstacaoRepository _estacoesRepository;
        public EstacaoBusiness(IEstacaoRepository estacoesRepository)
        {
            _estacoesRepository = estacoesRepository;
        }

        public IEnumerable<EstacaoDTO> ObterEstacaoPorIP(string endereco)
        {
            IEnumerable<EstacaoEntity> estacaoEntity = null;
            estacaoEntity = _estacoesRepository.EstacaoPorIP(endereco);

            if (estacaoEntity.Count() > 0)
            {
                DateTime dataAtualizacao = estacaoEntity.FirstOrDefault().DataAtualizacao;
                if (dataAtualizacao.Date < DateTime.Now.AddDays(-1).Date)
                    throw new DataAtualizacaoException(dataAtualizacao.Date.ToString());
            }

            estacaoEntity = estacaoEntity.Where(e => e.Filial > 0);

            IEnumerable<EstacaoDTO> retorno = Mapper.Map<IEnumerable<EstacaoEntity>, IEnumerable<EstacaoDTO>>(estacaoEntity);
            return retorno;
        }

        public string PingFilialOnline()
        {
            return _estacoesRepository.PingFilialOnline();
        }
    }
}