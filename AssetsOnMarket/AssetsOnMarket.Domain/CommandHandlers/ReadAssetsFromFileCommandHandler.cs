using AssetsOnMarket.Domain.Commands;
using AssetsOnMarket.Domain.Interfaces;
using AssetsOnMarket.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AssetsOnMarket.Domain.CommandHandlers
{
    public class ReadAssetsFromFileCommandHandler : IRequestHandler<ReadAssetsFromFileCommand, int>
    {
        private readonly IAssetRepository _assetRepository;

        public ReadAssetsFromFileCommandHandler(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public Task<int> Handle(ReadAssetsFromFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //if (request.AssetId <= 0)
                //    throw new ArgumentException("assetId <= 0 ");

                //// Other validations could come here

                //AssetProperty assetProperty = new AssetProperty()
                //{
                //    AssetId = request.AssetId,
                //    Property = request.Property,
                //    Value = request.Value,
                //    Timestamp = request.Timestamp
                //};

                //_assetRepository.AddOrUpdateAssetProperty(assetProperty);
                //return _assetRepository.SaveChangesAsync();
                return Task.FromResult(1);
            }
            catch (Exception)
            {
                throw;                
            }
        }
    }
}
