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
        IEnumerable<Asset> GetAssetsNoTracking(Expression<Func<Asset, bool>> filter = null);
        IEnumerable<AssetProperty> GetAssetPropertyNoTracking(Expression<Func<AssetProperty, bool>> filter = null);
        Task<int> AddOrUpdateAssetProperty(AssetProperty assetProperty);
        Task InsertAsset(Asset asset);
        Task InsertAssetProperty(AssetProperty assetProperty);
        Task BulkInsertUpdate(List<AssetProperty> assetsOnFile, int maxBatchSize);
        int SaveChanges();
    }
}
