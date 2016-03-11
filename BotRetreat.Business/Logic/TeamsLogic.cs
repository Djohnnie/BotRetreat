using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat.Business.Base;
using BotRetreat.Business.Interfaces;
using BotRetreat.DataAccess;
using BotRetreat.Domain;
using BotRetreat.Mappers;
using TeamEntity = BotRetreat.Domain.Team;
using TeamDto = BotRetreat.DataTransferObjects.Team;
using BotRetreat.Business.Exceptions;

namespace BotRetreat.Business.Logic
{
    public class TeamsLogic : LogicBase<IBotRetreatContext>, ITeamsLogic
    {
        private readonly IMapper<TeamEntity, TeamDto> _teamMapper;
        private readonly IStatisticsLogic _statisticsLogic;

        public TeamsLogic(IBotRetreatContext dbContext, IMapper<TeamEntity, TeamDto> teamMapper, IStatisticsLogic statisticsLogic) : base(dbContext)
        {
            _teamMapper = teamMapper;
            _statisticsLogic = statisticsLogic;
        }

        public async Task<List<TeamDto>> GetAllTeams()
        {
            var teams = _teamMapper.Map(await _dbContext.Teams.ToListAsync());
            foreach (var team in teams)
            {
                team.Statistics = await _statisticsLogic.GetTeamStatistics(team.Name, team.Password);
                team.Password = String.Empty;
            }
            return teams;
        }

        public async Task<TeamDto> GetTeam(String name, String password)
        {
            var existingTeam = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name.ToUpper() == name.ToUpper() && x.Password == password);
            return _teamMapper.Map(existingTeam);
        }

        public async Task<TeamDto> CreateTeam(TeamDto team)
        {
            var existingTeam = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name.ToUpper() == team.Name.ToUpper());
            if (existingTeam != null)
            {
                throw new BusinessException($"A team with name '{team}' already exists.");
            }
            var existingArena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name.ToUpper() == team.Name.ToUpper());
            if (existingArena != null)
            {
                throw new BusinessException($"An arena with name '{team}' already exists.");
            }
            var domainTeam = _teamMapper.Map(team);
            _dbContext.Teams.Add(domainTeam);
            _dbContext.Arenas.Add(new Arena
            {
                Active = true,
                DeploymentRestriction = TimeSpan.Zero,
                MaximumPoints = 1000,
                Width = 10,
                Height = 10,
                Name = team.Name,
                LastRefreshDateTime = DateTime.UtcNow,
                Private = true
            });
            await _dbContext.SaveChangesAsync();
            return team;
        }

        public async Task<TeamDto> EditTeam(TeamDto team)
        {
            _dbContext.Teams.Attach(_teamMapper.Map(team));
            await _dbContext.SaveChangesAsync();
            return _teamMapper.Map(
                await _dbContext.Teams.SingleOrDefaultAsync(x => x.Id == team.Id));
        }

        public async Task RemoveTeam(Guid teamId)
        {
            var existingTeam = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Id == teamId);
            if (existingTeam != null)
            {
                var bots = await _dbContext.Deployments.Where(x => x.Team.Id == existingTeam.Id).Select(x => x.Bot).ToListAsync();
                var deployments = await _dbContext.Deployments.Where(x => x.Team.Id == existingTeam.Id).ToListAsync();
                deployments.ForEach(deployment => _dbContext.Deployments.Remove(deployment));
                bots.ForEach(bot => _dbContext.Bots.Remove(bot));
                var existingArena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == existingTeam.Name);
                if (existingArena != null)
                {
                    _dbContext.Arenas.Remove(existingArena);
                }
                _dbContext.Teams.Remove(existingTeam);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}