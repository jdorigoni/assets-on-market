using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace AssetsOnMarket.Domain.Core.Events
{
    public abstract class Message : IRequest<int>
    {
        public string MessageType { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
