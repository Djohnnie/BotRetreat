using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Business.Interfaces
{
    public interface IBotsLogic : ILogic
    {
        Task<List<Bot>> GetAllBots();

        Task<Bot> CreateBot(Bot bot);

        Task<Bot> EditBot(Bot bot);

        Task RemoveBot(Guid botId);
    }
}