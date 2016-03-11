using System;

namespace BotRetreat.Domain
{
    public class Health : IEntity
    {
        public Int16 Maximum { get; set; }

        public Int16 Current { get; set; }

        public Int16 Drain { get; set; }
    }
}