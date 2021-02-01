using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AssetsOnMarket.Domain.Models;

namespace AssetsOnMarket.Domain.Interfaces
{
    public interface IAssetRepository
    {
        IEnumerable<Asset> GetAssets(Expression<Func<Asset, bool>> filter = null);
        IEnumerable<AssetProperty> GetAssetProperty(Expression<Func<AssetProperty, bool>> filter = null);
        Task AddOrUpdateAssetProperty(AssetProperty assetProperty);
        Task InsertAsset(Asset asset);
        Task InsertAssetProperty(AssetProperty assetProperty);
        int SaveChanges();
    }
}
