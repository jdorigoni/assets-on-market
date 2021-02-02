using AutoMapper;
using AssetsOnMarket.Application.Interfaces;
using AssetsOnMarket.Application.ViewModels;
using AssetsOnMarket.Domain.Commands;
using AssetsOnMarket.Domain.Core.Bus;
using System.Threading.Tasks;
using System.Collections.Generic;
using AssetsOnMarket.Domain.Queries;
using System;
using Serilog;

namespace AssetsOnMarket.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IMediatorHandler _bus;
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;

        public AssetService(IMediatorHandler bus, 
                            IMapper autoMapper, 
                            ILogger logger)
        {
            _bus = bus;
            _autoMapper = autoMapper;
            _logger = logger;
        }

        public async Task ReadAssetsFromFile(int batchSize)
        {
            _logger.Information("Star reading assets from File CSV...");
            
            var readAssetsFromFileCommand = new ReadAssetsFromFileCommand() { MaxBatchSize = batchSize};
           
            await _bus.SendCommand(readAssetsFromFileCommand);
           
            _logger.Information("End reading assets from File CSV...");
        }

        public async Task AddOrUpdateAsync(AssetPropertyViewModel assetPropertyViewModel)
        {
            if (assetPropertyViewModel == null)
                throw new ArgumentNullException("asset property request");
            await _bus.SendCommand(_autoMapper.Map<UpdateAssetPropertyCommand>(assetPropertyViewModel));
        }

        public async Task<IEnumerable<int>> GetAssetsIdsByPropertyValue(PropertyValueViewModel propertyValueVM)
        {
            return await _bus.SendQuery(_autoMapper.Map<AssetsIdsByPropertyValueQuery>(propertyValueVM));
        }
    }
}
