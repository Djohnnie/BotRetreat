using System;

namespace BotRetreat.DataTransferObjects
{
    public class Deployment : IDataTransferObject
    {
        public Guid Id { get; set; }
        public String BotName { get; set; }
        public String TeamName { get; set; }
        public String ArenaName { get; set; }
    }
}