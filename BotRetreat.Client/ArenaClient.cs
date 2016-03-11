using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.Client.Base;
using BotRetreat.Client.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Routes;

namespace BotRetreat.Client
{
    public class ArenaClient : ClientBase, IArenaClient
    {
        public Task<List<Arena>> GetAllArenas()
        {
            return Get<List<Arena>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_ARENAS}");
        }

        public Task<List<ArenaList>> GetArenasList()
        {
            return Get<List<ArenaList>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_ARENAS_LIST}");
        }

        public Task<List<Arena>> GetAvailableArenas()
        {
            return Get<List<Arena>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_AVAILABLE_ARENAS}");
        }

        public Task<Arena> GetTeamArena(String teamName, String teamPassword)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(teamName)] = teamName,
                [nameof(teamPassword)] = teamPassword
            };
            return Get<Arena>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_TEAM_ARENA}", parameters);
        }

        public Task<List<Arena>> GetTeamArenas(String teamName, String teamPassword)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(teamName)] = teamName,
                [nameof(teamPassword)] = teamPassword
            };
            return Get<List<Arena>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_TEAM_ARENAS}", parameters);
        }

        public Task<Arena> CreateArena(Arena arena)
        {
            return Post<Arena, Arena>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_ARENA}", arena);
        }

        public Task<Arena> EditArena(Arena arena)
        {
            return Put<Arena, Arena>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.PUT_ARENA}", arena);
        }

        public Task RemoveArena(Guid arenaId)
        {
            return Delete(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.DELETE_ARENA}", arenaId);
        }
    }
}