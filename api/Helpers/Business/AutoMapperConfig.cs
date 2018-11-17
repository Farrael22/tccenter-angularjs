﻿using AutoMapper;
using tccenter.api.Domain.DTO;
using tccenter.api.Domain.Entity;

namespace tccenter.api.Helpers.Business
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<LoginEntity, LoginDTO>();
                cfg.CreateMap<UsuarioEntity, UsuarioDTO>();
                cfg.CreateMap<UsuarioDTO, UsuarioEntity>();
                cfg.CreateMap<TopicosInteressantesEntity, TopicosInteressantesDTO>();
                cfg.CreateMap<TopicoMestreEntity, TopicoMestreDTO>();
                cfg.CreateMap<PublicacaoEntity, PublicacaoDTO>();
                cfg.CreateMap<OrientadorEntity, OrientadorDTO>();
                cfg.CreateMap<MensagemDTO, MensagemEntity>();
            });
        }

        public static void ResetMappings()
        {
            Mapper.Reset();
        }
    }
}