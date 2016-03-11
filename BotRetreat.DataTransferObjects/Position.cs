using System;

namespace BotRetreat.DataTransferObjects
{
    public class Position : IDataTransferObject
    {
        public Int16 X { get; set; }
        public Int16 Y { get; set; }
    }
}