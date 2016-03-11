using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Arena.Wpf.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<List<History>> GetHistoryByArena(String arenaName, DateTime? fromDateTime = null, DateTime? untilDateTime = null);

        Task<List<History>> GetHistoryByBot(String botName, DateTime? fromDateTime = null, DateTime? untilDateTime = null);
    }
}