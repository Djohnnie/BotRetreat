using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.Client.Base;
using BotRetreat.Client.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Routes;

namespace BotRetreat.Client
{
    public class TeamClient : ClientBase, ITeamClient
    {
        public Task<List<Team>> GetAllTeams()
        {
            return Get<List<Team>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_TEAMS}");
        }

        public Task<Team> GetTeam(String name, String password)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(name)] = name,
                [nameof(password)] = password
            };
            return Get<Team>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_TEAM}", parameters);
        }

        public Task<Team> CreateTeam(Team team)
        {
            return Post<Team, Team>(
               $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_TEAM}", team);
        }

        public Task<Team> EditTeam(Team team)
        {
            return Put<Team, Team>(
               $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.PUT_TEAM}", team);
        }

        public Task RemoveTeam(Guid teamId)
        {
            return Delete(
               $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.DELETE_TEAM}", teamId);
        }
    }
}