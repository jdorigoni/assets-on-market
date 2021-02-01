using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AssetsOnMarket.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AssetsOnMarket.Api.Configurations
{
    public static class AutoMapperConfig
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AssetsOnMarket.Application.AutoMapper.AutoMapperConfiguration));
            AutoMapperConfiguration.RegisterMappings();
        }
    }
}
