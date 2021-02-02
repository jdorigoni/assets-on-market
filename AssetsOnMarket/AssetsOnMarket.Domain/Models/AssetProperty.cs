using System;
using System.Collections.Generic;
using System.Text;

namespace AssetsOnMarket.Domain.Models
{
    public class AssetProperty
    {
        #region [Properties]
        public int? Id { get; set; }
        public int AssetId { get; set; }
        public string Property { get; set; }
        public bool Value { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual Asset Asset { get; set; }
        #endregion
    }
}
