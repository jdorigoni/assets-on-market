using System.Collections.Generic;
using System.Threading.Tasks;
using AssetsOnMarket.Domain.Core.Commands;
using AssetsOnMarket.Domain.Core.Queries;

namespace AssetsOnMarket.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;

        Task<IEnumerable<int>> SendQuery<T>(T query) where T : Query;
    }
}
