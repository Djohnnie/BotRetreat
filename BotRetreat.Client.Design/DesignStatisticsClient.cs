using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.Client.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Enums;
using BotRetreat.Utilities;

namespace BotRetreat.Client.Design
{
    public class DesignStatisticsClient : BaseDesignClient, IStatisticsClient
    {
        public Task<List<TeamStatistic>> GetTeamStatistics(String teamName, String teamPassword)
        {
            return Task.FromResult(new List<TeamStatistic>
            {
                RandomTeamStatistic("Arena 1", teamName),
                RandomTeamStatistic("Arena 2", teamName),
                RandomTeamStatistic("Arena 3", teamName)
            });
        }

        public Task<List<BotStatistic>> GetBotStatistics(String teamName, String teamPassword, String arenaName)
        {
            return Task.FromResult(new List<BotStatistic>
            {
                RandomBotStatistic("Bot 1","Arena 1"),
                RandomBotStatistic("Bot 2","Arena 2"),
                RandomBotStatistic("Bot 3","Arena 3")
            });
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

        private BotStatistic RandomBotStatistic(String botName, String arenaName)
        {
            return new BotStatistic
            {
                BotName = botName,
                ArenaName = arenaName,
                PhysicalHealth = new Health { Maximum = RandomInt16(), Current = RandomInt16(), Drain = RandomByte() },
                Stamina = new Health { Maximum = RandomInt16(), Current = RandomInt16(), Drain = RandomByte() },
                LastAction = RandomEnum<LastAction>(),
                Location = new Position { X = RandomByte(), Y = RandomByte() },
                Orientation = RandomEnum<Orientation>(),
                Script = "MoveForward();".Base64Encode(),
                TotalPhysicalDamageDone = RandomInt16(),
                TotalNumberOfKills = RandomByte(),
                BotLife = RandomTimeSpan()
            };
        }
    }
}