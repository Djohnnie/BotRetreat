using System;

namespace BotRetreat.Domain
{
    public class Position : IEntity
    {
        public Int16 X { get; set; }
        public Int16 Y { get; set; }
    }
}