using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.Client.Interfaces;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Client.Design
{
    public class DesignTeamClient : BaseDesignClient, ITeamClient
    {
        public Task<List<Team>> GetAllTeams()
        {
            return Task.FromResult(new List<Team>()
            {
                new Team {Name = "Team 1", Statistics = new List<TeamStatistic>
                {
                    RandomTeamStatistic("Arena 1", "Team 1"),
                    RandomTeamStatistic("Arena 2", "Team 1"),
                }},
                new Team {Name = "Team 2", Statistics = new List<TeamStatistic>
                {
                    RandomTeamStatistic("Arena 1", "Team 2"),
                    RandomTeamStatistic("Arena 2", "Team 2"),
                }},
                new Team {Name = "Team 3", Statistics = new List<TeamStatistic>
                {
                    RandomTeamStatistic("Arena 1", "Team 3"),
                    RandomTeamStatistic("Arena 2", "Team 3"),
                }}
            });
        }

        public Task<Team> GetTeam(String name, String password)
        {
            return Task.FromResult(new Team { Name = name, Password = password });
        }

        public Task<Team> CreateTeam(Team team)
        {
            return Task.FromResult(team);
        }

        public Task<Team> EditTeam(Team team)
        {
            return Task.FromResult(team);
        }

        public Task RemoveTeam(Guid teamId)
        {
            return Task.CompletedTask;
        }

        private TeamStatistic RandomTeamStatistic(String arenaName, String teamName)
        {
            return new TeamStatistic
            {
                ArenaName = arenaName,
                TeamName = teamName,
                NumberOfDeployments = RandomInt16(),
                NumberOfLiveBots = RandomInt16(),
                NumberOfDeadBots = RandomInt16(),
                TotalNumberOfKills = RandomInt16(),
                TotalNumberOfDeaths = RandomInt16(),
                TotalPhysicalDamageDone = RandomInt32(),
                TotalStaminaConsumed = RandomInt32(),
                AverageBotLife = RandomTimeSpan()
            };
        }
    }
}