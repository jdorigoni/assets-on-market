using System;
using System.Collections.Generic;
using System.Text;

namespace AssetsOnMarket.Domain.Models
{
    public class AssetProperty
    {
        public int AssetId { get; set; }
        public string Property { get; set; }
        public bool Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
