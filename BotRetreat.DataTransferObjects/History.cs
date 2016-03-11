using System;
using BotRetreat.Enums;

namespace BotRetreat.DataTransferObjects
{
    public class History : IDataTransferObject
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public HistoryType Type { get; set; }

        public String ArenaName { get; set; }

        public String BotName { get; set; }

        public DateTime DateTime { get; set; }
    }
}