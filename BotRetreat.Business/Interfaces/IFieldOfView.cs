using System.Collections.Generic;

namespace BotRetreat.Business.Interfaces
{
    public interface IFieldOfView
    {
        List<IBot> Bots { get; }
        List<IBot> FriendlyBots { get; }
        List<IBot> EnemyBots { get; }
        List<IBot> PredatorBots { get; }
    }
}