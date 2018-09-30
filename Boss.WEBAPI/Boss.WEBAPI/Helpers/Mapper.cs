using AutoMapper;
using Dto;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boss.WEBAPI.Helpers
{
    public static class MapperConfig
    {
        public static IMapper iMapper
        {
            get
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Dto.Product, Entity.Model.Product>();
                });

                return config.CreateMapper();
            }
        }
    }
}