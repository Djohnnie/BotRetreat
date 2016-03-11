using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.Client.Interfaces;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Client.Design
{
    public class DesignArenaClient : BaseDesignClient, IArenaClient
    {
        public Task<List<Arena>> GetAllArenas()
        {
            return Task.FromResult(new List<Arena>
            {
                RandomArena("Arena 1"),
                RandomArena("Arena 2"),
                RandomArena("Arena 3")
            });
        }

        public Task<List<ArenaList>> GetArenasList()
        {
            return Task.FromResult(new List<ArenaList>
            {
                RandomArenaList("Arena 1"),
                RandomArenaList("Arena 2"),
                RandomArenaList("Arena 3")
            });
        }

        public Task<List<Arena>> GetAvailableArenas()
        {
            return Task.FromResult(new List<Arena>
            {
                RandomArena("Arena 1"),
                RandomArena("Arena 2"),
                RandomArena("Arena 3")
            });
        }

        public Task<Arena> GetTeamArena(String teamName, String teamPassword)
        {
            return Task.FromResult(RandomArena(teamName));
        }

        public Task<List<Arena>> GetTeamArenas(String teamName, String teamPassword)
        {
            return Task.FromResult(new List<Arena>
            {
                RandomArena("Arena 1"),
                RandomArena("Arena 2"),
                RandomArena("Arena 3")
            });
        }

        public Task<Arena> CreateArena(Arena arena)
        {
            return Task.FromResult(arena);
        }

        public Task<Arena> EditArena(Arena arena)
        {
            return Task.FromResult(arena);
        }

        public Task RemoveArena(Guid arenaId)
        {
            return Task.CompletedTask;
        }

        private ArenaList RandomArenaList(String arenaName)
        {
            return new ArenaList
            {
                Name = arenaName
            };
        }

        private Arena RandomArena(String arenaName)
        {
            return new Arena
            {
                Name = arenaName,
                Active = true,
                Width = RandomByte(),
                Height = RandomByte(),
                LastRefreshDateTime = DateTime.UtcNow,
                Private = false,
                DeploymentRestriction = RandomTimeSpan(),
                LastDeploymentDateTime = RandomDateTime(),
                MaximumPoints = RandomInt16(),
            };
        }
    }
}