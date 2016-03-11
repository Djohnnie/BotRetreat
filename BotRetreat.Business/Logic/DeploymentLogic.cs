using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat.Business.Base;
using BotRetreat.Business.Exceptions;
using BotRetreat.Business.Interfaces;
using BotRetreat.DataAccess;
using BotRetreat.Domain;
using BotRetreat.Enums;
using DeploymentEntity = BotRetreat.Domain.Deployment;
using DeploymentDto = BotRetreat.DataTransferObjects.Deployment;

namespace BotRetreat.Business.Logic
{
    public class DeploymentLogic : LogicBase<IBotRetreatContext>, IDeploymentLogic
    {
        private readonly ILogLogic _logLogic;
        public DeploymentLogic(IBotRetreatContext dbContext, ILogLogic logLogic) : base(dbContext)
        {
            _logLogic = logLogic;
        }

        public async Task<DeploymentDto> Deploy(DeploymentDto deployment)
        {
            var arena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == deployment.ArenaName);
            if (arena == null) { throw new BusinessException($"Arena '{deployment.ArenaName}' does not exist!"); }

            var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name == deployment.TeamName);
            if (team == null) { throw new BusinessException($"Team '{deployment.TeamName}' does not exist!"); }

            var bot = await _dbContext.Bots.SingleOrDefaultAsync(x => x.Name == deployment.BotName);
            if (bot == null) { throw new BusinessException($"Bot '{deployment.BotName}' does not exist!"); }

            var assignedPoints = bot.Stamina.Maximum + bot.PhysicalHealth.Maximum;
            if (assignedPoints > arena.MaximumPoints)
            {
                throw new BusinessException($"Number of assigned bot points ({assignedPoints}) is larger than maximum allowed ({arena.MaximumPoints})!");
            }

            var lastDeployment = await _dbContext.Deployments
                .Where(x => x.Team.Id == team.Id)
                .OrderByDescending(x => x.DeploymentDateTime)
                .FirstOrDefaultAsync(x => x.Arena.Id == arena.Id);

            if (lastDeployment != null && !team.Predator)
            {
                var timeSinceLastDeployment = DateTime.UtcNow - lastDeployment.DeploymentDateTime;
                if (timeSinceLastDeployment < arena.DeploymentRestriction && !bot.Predator)
                {
                    throw new BusinessException($"Deployment restriction of {arena.DeploymentRestriction} applies!");
                }
            }

            var existingBots = await _dbContext.Deployments.Where(x => x.Arena.Id == arena.Id)
                    .Select(x => x.Bot)
                    .Where(x => x.PhysicalHealth.Current > 0)
                    .Select(b => new { b.Location.X, b.Location.Y }).ToListAsync();
            var randomGenerator = new Random();
            var locationFound = false;
            while (!locationFound)
            {
                bot.Location = new Position
                {
                    X = (Int16)randomGenerator.Next(0, arena.Width),
                    Y = (Int16)randomGenerator.Next(0, arena.Height)
                };
                bot.Orientation = (Orientation)randomGenerator.Next(0, 4);
                if (!existingBots.Any(l => l.X == bot.Location.X && l.Y == bot.Location.Y))
                {
                    locationFound = true;
                }
            }

            var deploymentEntity = new DeploymentEntity
            {
                Arena = arena,
                Team = team,
                Bot = bot,
                DeploymentDateTime = DateTime.UtcNow
            };
            _dbContext.Deployments.Add(deploymentEntity);
            await _dbContext.SaveChangesAsync();
            _logLogic.LogMessage(arena, deploymentEntity, bot, HistoryName.BotDeployed);
            return deployment;
        }

        public async Task<Boolean> Available(String teamName, String arenaName)
        {
            var arena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == arenaName);
            var team = await _dbContext.Teams.SingleOrDefaultAsync(x => x.Name == teamName);

            var lastDeployment = await _dbContext.Deployments
                .Where(x => x.Team.Name == teamName)
                .OrderByDescending(x => x.DeploymentDateTime)
                .FirstOrDefaultAsync(x => x.Arena.Name == arenaName);

            if (team.Predator || lastDeployment == null)
            {
                return true;
            }

            var timeSinceLastDeployment = DateTime.UtcNow - lastDeployment.DeploymentDateTime;
            if (timeSinceLastDeployment > arena.DeploymentRestriction)
            {
                return true;
            }

            return false;
        }
    }
}