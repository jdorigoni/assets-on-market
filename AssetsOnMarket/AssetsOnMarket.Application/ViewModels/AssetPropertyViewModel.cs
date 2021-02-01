using System;
using System.Collections.Generic;
using System.Text;
using AssetsOnMarket.Domain.Models;

namespace AssetsOnMarket.Application.ViewModels
{
    public class AssetPropertyViewModel
    {
        public int AssetId { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }
        public string Timestamp { get; set; }

        public AssetPropertyViewModel(int assetId, string property, string value, string timestamp)
        {
            AssetId = assetId;
            Property = property;
            Value = value;
            Timestamp = timestamp;
        }
    }
}
