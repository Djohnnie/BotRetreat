using System;
using System.Threading.Tasks;
using BotRetreat.Business.Base;
using BotRetreat.Business.Interfaces;
using BotRetreat.DataAccess;
using BotRetreat.Domain;
using BotRetreat.Enums;

namespace BotRetreat.Business.Logic
{
    public class LogLogic : LogicBase<IBotRetreatHistoryContext>, ILogLogic
    {
        private readonly Object _syncRoot = new Object();

        public LogLogic(IBotRetreatHistoryContext dbContext) : base(dbContext)
        {
        }

        private void Log(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description, HistoryType type)
        {
            var history = new History
            {
                ArenaId = arena?.Id,
                DeploymentId = deployment?.Id,
                BotId = bot?.Id,
                Name = name.GetName(),
                Description = name.GetDescription(),
                DateTime = DateTime.UtcNow
            };
            lock (_syncRoot)
            {
                _dbContext.History.Add(history);
            }
        }

        private void Log(Arena arena, Deployment deployment, Bot bot, String description, HistoryType type)
        {
            var history = new History
            {
                ArenaId = arena?.Id,
                DeploymentId = deployment?.Id,
                BotId = bot?.Id,
                Name = "Timing",
                Description = description,
                DateTime = DateTime.UtcNow
            };
            _dbContext.History.Add(history);
        }

        public void LogMessage(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description = null)
        {
            Log(arena, deployment, bot, name, description, HistoryType.Message);
        }

        public void LogWarning(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description = null)
        {
            Log(arena, deployment, bot, name, description, HistoryType.Warning);
        }

        public void LogError(Arena arena, Deployment deployment, Bot bot, HistoryName name, String description = null)
        {
            Log(arena, deployment, bot, name, description, HistoryType.Error);
        }

        public void LogTiming(Arena arena, Deployment deployment, Bot bot, String description)
        {
            Log(arena, deployment, bot, description, HistoryType.Timing);
        }

        public void SaveChanges()
        {
            //_dbContext.SaveChangesAsync();
        }
    }
}