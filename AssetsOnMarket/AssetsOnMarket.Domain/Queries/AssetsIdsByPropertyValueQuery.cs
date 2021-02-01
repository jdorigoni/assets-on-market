using AssetsOnMarket.Domain.Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetsOnMarket.Domain.Queries
{
    public class AssetsIdsByPropertyValueQuery : Query
    {
        public AssetsIdsByPropertyValueQuery(string property, bool value)
        {
            Property = property;
            Value = value;
        }

        public string Property { get; set; }
        public bool Value { get; set; }
    }
}
