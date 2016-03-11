using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Business.Interfaces
{
    public interface IArenaLogic : ILogic
    {
        Task<List<Arena>> GetAllArenas();

        Task<List<ArenaList>> GetArenasList();

        Task<List<Arena>> GetAvailableArenas();

        Task<Arena> GetTeamArena(String teamName, String teamPassword);

        Task<List<Arena>> GetTeamArenas(String teamName, String teamPassword);

        Task<Arena> CreateArena(Arena arena);

        Task<Arena> EditArena(Arena arena);

        Task RemoveArena(Guid arenaId);
    }
}