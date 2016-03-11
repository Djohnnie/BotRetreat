using System;
using BotRetreat.Enums;

namespace BotRetreat.DataTransferObjects
{
    public class BotStatistic : IDataTransferObject
    {
        public Guid BotId { get; set; }

        public String BotName { get; set; }

        public Guid ArenaId { get; set; }

        public String ArenaName { get; set; }

        public Health PhysicalHealth { get; set; }

        public Health Stamina { get; set; }

        public LastAction LastAction { get; set; }

        public Position Location { get; set; }

        public Orientation Orientation { get; set; }

        public String Script { get; set; }

        public Int32 TotalPhysicalDamageDone { get; set; }

        public Int32 TotalNumberOfKills { get; set; }

        public TimeSpan BotLife { get; set; }
    }
}