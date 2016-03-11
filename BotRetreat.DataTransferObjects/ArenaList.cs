using System;

namespace BotRetreat.DataTransferObjects
{
    public class ArenaList : IDataTransferObject
    {
        public Guid Id { get; set; }

        public String Name { get; set; }
    }
}