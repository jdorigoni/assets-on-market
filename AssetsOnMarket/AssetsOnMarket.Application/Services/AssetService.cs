using AutoMapper;
using AssetsOnMarket.Application.Interfaces;
using AssetsOnMarket.Application.ViewModels;
using AssetsOnMarket.Domain.Commands;
using AssetsOnMarket.Domain.Core.Bus;
using AssetsOnMarket.Domain.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using AssetsOnMarket.Domain.Queries;
using System;

namespace AssetsOnMarket.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IMediatorHandler _bus;
        private readonly IMapper _autoMapper;

        public AssetService(IMediatorHandler bus, 
                            IMapper autoMapper)
        {
            _bus = bus;
            _autoMapper = autoMapper;
        }

        public async Task ReadAssetsFromFile()
        {
            var readAssetsFromFileCommand = new ReadAssetsFromFileCommand();
            await _bus.SendCommand(readAssetsFromFileCommand);
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
