using System;

namespace BotRetreat.Business.Logic
{
    public class BotStat
    {
        public Guid BotId { get; set; }
        public Int16 PhysicalHealth { get; set; }
        public Int16 MentalHealth { get; set; }
        public Int16 Stamina { get; set; }
    }
}