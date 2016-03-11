using System.Threading;
using System.Threading.Tasks;

namespace BotRetreat.Business.Interfaces
{
    public interface ICoreLogic
    {
        Task Go(CancellationToken cancellationToken = default(CancellationToken));
    }
}