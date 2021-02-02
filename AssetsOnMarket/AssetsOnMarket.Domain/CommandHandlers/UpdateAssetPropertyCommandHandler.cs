using AssetsOnMarket.Domain.Commands;
using AssetsOnMarket.Domain.Interfaces;
using AssetsOnMarket.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AssetsOnMarket.Domain.CommandHandlers
{
    public class UpdateAssetPropertyCommandHandler : IRequestHandler<UpdateAssetPropertyCommand, int>
    {
        private readonly IAssetRepository _assetRepository;

        public UpdateAssetPropertyCommandHandler(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<int> Handle(UpdateAssetPropertyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AssetId <= 0)
                    throw new ArgumentException("assetId <= 0 ");

                // Other validations could come here

                AssetProperty assetProperty = new AssetProperty()
                {
                    AssetId = request.AssetId,
                    Property = request.Property,
                    Value = request.Value,
                    Timestamp = request.Timestamp
                };

                return await _assetRepository.AddOrUpdateAssetProperty(assetProperty);
            }
            catch (Exception)
            {
                throw;                
            }
        }
    }
}
