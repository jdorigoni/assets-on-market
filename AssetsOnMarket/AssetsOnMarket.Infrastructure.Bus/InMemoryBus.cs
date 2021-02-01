using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AssetsOnMarket.Domain.Core.Bus;
using AssetsOnMarket.Domain.Core.Commands;
using AssetsOnMarket.Domain.Core.Queries;
using MediatR;

namespace AssetsOnMarket.Infrastructure.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task<IEnumerable<int>> SendQuery<T>(T query) where T : Query
        {
            return _mediator.Send(query);
        }
    }
}
