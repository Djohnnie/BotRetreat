using System;

namespace BotRetreat.DataTransferObjects
{
    public class TeamStatistic : IDataTransferObject
    {
        public Guid TeamId { get; set; }

        public String TeamName { get; set; }

        public Guid ArenaId { get; set; }

        public String ArenaName { get; set; }

        public Int32 NumberOfDeployments { get; set; }

        public Int32 NumberOfLiveBots { get; set; }

        public Int32 NumberOfDeadBots { get; set; }

        public TimeSpan AverageBotLife { get; set; }

        public Int32 TotalNumberOfKills { get; set; }

        public Int32 TotalNumberOfDeaths { get; set; }

        public Int32 TotalPhysicalDamageDone { get; set; }

        public Int32 TotalStaminaConsumed { get; set; }
    }
}