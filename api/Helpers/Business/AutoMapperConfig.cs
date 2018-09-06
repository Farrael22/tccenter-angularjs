using balcao.offline.api.DTO;
using balcao.offline.api.Entity;
using AutoMapper;

namespace balcao.offline.api.Helpers.Business
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ProdutoEntity, ProdutoDTO>();
            });
        }

        public static void ResetMappings()
        {
            Mapper.Reset();
        }
    }
}