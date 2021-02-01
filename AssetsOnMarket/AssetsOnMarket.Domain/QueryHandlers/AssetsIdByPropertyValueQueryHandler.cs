using AssetsOnMarket.Domain.Interfaces;
using AssetsOnMarket.Domain.Models;
using AssetsOnMarket.Domain.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssetsOnMarket.Domain.QueryHandlers
{
    public class AssetsIdByPropertyValueQueryHandler : IRequestHandler<AssetsIdsByPropertyValueQuery, IEnumerable<int>>
    {
        private readonly IAssetRepository _assetRepository;

        public AssetsIdByPropertyValueQueryHandler(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<IEnumerable<int>> Handle(AssetsIdsByPropertyValueQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null) 
                    throw new ArgumentNullException($"assetsIdsByPropertyValueQuery");

                // Other validations could come here
                return (await _assetRepository.GetAssetProperty(ap => 
                                        ap.Property == request.Property && ap.Value == request.Value)
                                ).ToList()
                                 .Select(ap => ap.AssetId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}