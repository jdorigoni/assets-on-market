using System.Collections.Generic;
using System.Threading.Tasks;
using AssetsOnMarket.Application.ViewModels;

namespace AssetsOnMarket.Application.Interfaces
{
    public interface IAssetService
    {
        Task ReadAssetsFromFile();

        Task AddOrUpdateAsync(AssetPropertyViewModel assetPropertyViewModel);

        Task<IEnumerable<int>> GetAssetsIdsByPropertyValue(PropertyValueViewModel propertyValueVM);
    }
}
