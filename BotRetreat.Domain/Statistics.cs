using System;

namespace BotRetreat.Domain
{
    public class Statistics : IEntity
    {
        public Int32 PhysicalDamageDone { get; set; }

        public Int32 Kills { get; set; }

        public DateTime TimeOfBirth { get; set; }

        public DateTime? TimeOfDeath { get; set; }
    }
}