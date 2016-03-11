using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat.Business.Base;
using BotRetreat.Business.Extensions;
using BotRetreat.Business.Interfaces;
using BotRetreat.DataAccess;
using BotRetreat.Mappers;
using TeamEntity = BotRetreat.Domain.Team;
using TeamStatisticDto = BotRetreat.DataTransferObjects.TeamStatistic;
using BotEntity = BotRetreat.Domain.Bot;
using BotStatisticDto = BotRetreat.DataTransferObjects.BotStatistic;

namespace BotRetreat.Business.Logic
{
    public class StatisticsLogic : LogicBase<IBotRetreatContext>, IStatisticsLogic
    {
        private readonly IMapper<TeamEntity, TeamStatisticDto> _teamMapper;
        private readonly IMapper<BotEntity, BotStatisticDto> _botMapper;

        public StatisticsLogic(IBotRetreatContext dbContext, IMapper<TeamEntity, TeamStatisticDto> teamMapper, IMapper<BotEntity, BotStatisticDto> botMapper) : base(dbContext)
        {
            _teamMapper = teamMapper;
            _botMapper = botMapper;
        }

        public async Task<List<TeamStatisticDto>> GetTeamStatistics(String teamName, String teamPassword)
        {
            var teamStatistics = new List<TeamStatisticDto>();
            var team = await _dbContext.Teams
                .SingleOrDefaultAsync(x => x.Name.ToUpper() == teamName.ToUpper() && x.Password == teamPassword);
            if (team == null) return null;
            var arenas = await _dbContext.Arenas
                .SelectMany(x => x.Deployments)
                .Select(x => x.Arena)
                .Where(x => x.Active && (!x.Private || x.Name == teamName))
                .Include(x => x.Deployments)
                .Include(x => x.Deployments.Select(y => y.Bot))
                .Distinct().ToListAsync();
            arenas.ForEach(arena =>
            {
                var bots = arena.Deployments.Where(x => x.Team.Id == team.Id).Select(x => x.Bot).ToList();
                var teamStatistic = _teamMapper.Map(team);
                teamStatistic.ArenaId = arena.Id;
                teamStatistic.ArenaName = arena.Name;
                teamStatistic.TeamId = team.Id;
                teamStatistic.TeamName = team.Name;
                teamStatistic.NumberOfDeployments = arena.Deployments.Count(x => x.Team.Id == team.Id);
                teamStatistic.NumberOfLiveBots = bots.Count(x => x.PhysicalHealth.Current > 0);
                teamStatistic.NumberOfDeadBots = bots.Count(x => x.PhysicalHealth.Current == 0);
                var averageBotLife = bots.Where(x => x.Statistics.TimeOfDeath.HasValue).Select(x => (x.Statistics.TimeOfDeath.Value - x.Statistics.TimeOfBirth).TotalMilliseconds).AverageOrDefault(Double.MaxValue);
                teamStatistic.AverageBotLife = averageBotLife == Double.MaxValue ? TimeSpan.MaxValue : TimeSpan.FromMilliseconds(averageBotLife);
                teamStatistic.TotalNumberOfKills = bots.Select(x => x.Statistics.Kills).Sum();
                teamStatistic.TotalNumberOfDeaths = bots.Count(x => x.PhysicalHealth.Current == 0);
                teamStatistic.TotalPhysicalDamageDone = bots.Select(x => x.Statistics.PhysicalDamageDone).Sum();
                teamStatistic.TotalStaminaConsumed = bots.Select(x => x.Stamina.Maximum - x.Stamina.Current).Sum();
                teamStatistics.Add(teamStatistic);
            });
            return teamStatistics;
        }

        public async Task<List<BotStatisticDto>> GetBotStatistics(String teamName, String teamPassword, String arenaName)
        {
            var botStatistics = new List<BotStatisticDto>();
            var team = await _dbContext.Teams
                .SingleOrDefaultAsync(x => x.Name.ToUpper() == teamName.ToUpper() && x.Password == teamPassword);
            if (team == null) return null;
            var arena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == arenaName);
            if (arena == null) return null;
            var bots = await _dbContext.Deployments.Where(x => x.Arena.Id == arena.Id).Select(x => x.Bot).ToListAsync();
            bots.ForEach(bot =>
            {
                var botStatistic = _botMapper.Map(bot);
                botStatistic.BotId = bot.Id;
                botStatistic.BotName = bot.Name;
                botStatistic.ArenaId = arena.Id;
                botStatistic.ArenaName = arena.Name;
                botStatistic.TotalPhysicalDamageDone = bot.Statistics.PhysicalDamageDone;
                botStatistic.TotalNumberOfKills = bot.Statistics.Kills;
                botStatistic.BotLife = DateTime.UtcNow - bot.Statistics.TimeOfBirth;
                botStatistics.Add(botStatistic);
            });
            return botStatistics;
        }
    }
}