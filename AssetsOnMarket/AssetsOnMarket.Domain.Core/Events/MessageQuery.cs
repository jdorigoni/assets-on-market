using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetsOnMarket.Domain.Core.Events
{
    public abstract class MessageQuery : IRequest<IEnumerable<int>>
    {
        public string MessageType { get; protected set; }

        protected MessageQuery()
        {
            MessageType = GetType().Name;
        }
    }
}
