using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Client.Interfaces
{
    public interface ITeamClient
    {
        Task<List<Team>> GetAllTeams();

        Task<Team> GetTeam(String name, String password);

        Task<Team> CreateTeam(Team team);

        Task<Team> EditTeam(Team team);

        Task RemoveTeam(Guid teamId);
    }
}