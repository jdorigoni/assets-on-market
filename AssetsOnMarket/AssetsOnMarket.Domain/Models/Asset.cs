using System;
using System.Collections.Generic;
using System.Text;

namespace AssetsOnMarket.Domain.Models
{
    public class Asset
    {
        #region [Properties]
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public virtual ICollection<AssetProperty> AssetProperties { get; set; }
        #endregion

        #region [Constructors]
        public Asset()
        {
            AssetProperties = new HashSet<AssetProperty>();
        }
        #endregion 
    }
}
