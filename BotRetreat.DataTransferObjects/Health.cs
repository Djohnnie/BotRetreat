using System;

namespace BotRetreat.DataTransferObjects
{
    public class Health : IDataTransferObject
    {
        public Int16 Maximum { get; set; }

        public Int16 Current { get; set; }

        public Int16 Drain { get; set; }
    }
}