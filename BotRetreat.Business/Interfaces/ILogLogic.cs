using System;
using System.Threading.Tasks;
using BotRetreat.Domain;
using BotRetreat.Enums;

namespace BotRetreat.Business.Interfaces
{
    public interface ILogLogic
    {
        void LogMessage(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description = null);

        void LogWarning(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description = null);

        void LogError(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description = null);

        void LogTiming(Arena arena, Deployment deployment, Bot bot, String description);

        void SaveChanges();
    }
}