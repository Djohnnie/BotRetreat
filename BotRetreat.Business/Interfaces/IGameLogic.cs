using System;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Business.Interfaces
{
    public interface IGameLogic : ILogic
    {
        Task<Game> GetGameForArena(String arenaName);
    }
}