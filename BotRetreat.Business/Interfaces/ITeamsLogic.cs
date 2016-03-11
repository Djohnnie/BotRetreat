using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Business.Interfaces
{
    public interface ITeamsLogic : ILogic
    {
        Task<List<Team>> GetAllTeams();

        Task<Team> GetTeam(String name, String password);

        Task<Team> CreateTeam(Team team);

        Task<Team> EditTeam(Team team);

        Task RemoveTeam(Guid teamId);
    }
}