using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.Arena.Wpf.Services.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Enums;

namespace BotRetreat.Arena.Wpf.Services.Design
{
    public class DesignHistoryService : IHistoryService
    {
        public Task<List<History>> GetHistoryByArena(String arenaName, DateTime? fromDateTime = null, DateTime? untilDateTime = null)
        {
            return GetDesignData();
        }

        public Task<List<History>> GetHistoryByBot(String botName, DateTime? fromDateTime = null, DateTime? untilDateTime = null)
        {
            return GetDesignData();
        }

        private static Task<List<History>> GetDesignData()
        {
            return Task.Run(() => new List<History>
            {
                new History
                {
                    ArenaName = "Arena 1",
                    BotName = "Bot 1",
                    DateTime = DateTime.Now,
                    Name = "Name 1",
                    Description = "Description 1",
                    Type = HistoryType.Message
                },
                new History
                {
                    ArenaName = "Arena 2",
                    BotName = "Bot 2",
                    DateTime = DateTime.Now,
                    Name = "Name 2",
                    Description = "Description 2",
                    Type = HistoryType.Warning
                },
                new History
                {
                    ArenaName = "Arena 3",
                    BotName = "Bot 3",
                    DateTime = DateTime.Now,
                    Name = "Name 3",
                    Description = "Description 3",
                    Type = HistoryType.Error
                },
            });
        }
    }
}