using System;

namespace AssetsOnMarket.Domain.Commands
{
    public class UpdateAssetPropertyCommand : AssetPropertyCommand
    {
        public UpdateAssetPropertyCommand(int assetId, string property, bool value, DateTime timestamp)
        {
            AssetId = assetId;
            Property = property;
            Value = value;
            Timestamp = timestamp;
        }
    }
}
