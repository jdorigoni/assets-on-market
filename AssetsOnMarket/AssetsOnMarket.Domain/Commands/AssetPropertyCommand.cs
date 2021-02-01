using System;
using System.Collections.Generic;
using System.Text;
using AssetsOnMarket.Domain.Core.Commands;

namespace AssetsOnMarket.Domain.Commands
{
    public abstract class AssetPropertyCommand : Command
    {
        public int AssetId { get; protected set; }
        public string Property { get; protected set; }
        public bool Value { get; protected set; }
        public DateTime Timestamp { get; protected set; }
    }
}
