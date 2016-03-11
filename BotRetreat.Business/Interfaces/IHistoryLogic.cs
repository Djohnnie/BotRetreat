using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Business.Interfaces
{
    public interface IHistoryLogic : ILogic
    {
        Task<List<History>> GetHistoryByArenaId(Guid arenaId, DateTime? fromDateTime = null, DateTime? untilDateTime = null);

        Task<List<History>> GetHistoryByBotId(Guid botId, DateTime? fromDateTime = null, DateTime? untilDateTime = null);
    }
}