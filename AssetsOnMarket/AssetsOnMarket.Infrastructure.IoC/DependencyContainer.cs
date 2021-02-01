using System;
using System.Collections.Generic;
using System.Text;
using AssetsOnMarket.Application.Interfaces;
using AssetsOnMarket.Application.Services;
using AssetsOnMarket.Domain.CommandHandlers;
using AssetsOnMarket.Domain.Commands;
using AssetsOnMarket.Domain.Core.Bus;
using AssetsOnMarket.Domain.Interfaces;
using AssetsOnMarket.Domain.Queries;
using AssetsOnMarket.Domain.QueryHandlers;
using AssetsOnMarket.Infrastructure.Bus;
using AssetsOnMarket.Infrastructure.Data.Context;
using AssetsOnMarket.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AssetsOnMarket.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain InMemoryBus MediatR
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain Handlers MediatR
            services.AddScoped<IRequestHandler<ReadAssetsFromFileCommand, int>, ReadAssetsFromFileCommandHandler>();
            services.AddScoped<IRequestHandler<AssetsIdsByPropertyValueQuery, IEnumerable<int>>, AssetsIdByPropertyValueQueryHandler>();
            services.AddScoped<IRequestHandler<UpdateAssetPropertyCommand, int>, UpdateAssetPropertyCommandHandler>();

            // Application Layer
            services.AddScoped<IAssetService, AssetService>();

            // Infrastructure Data Layer
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<AssetsOnMarketDBContext>();
        }
    }
}
