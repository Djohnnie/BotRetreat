using System;
using BotRetreat.Enums;

namespace BotRetreat.Domain
{
    public class History : IEntity
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public HistoryType Type { get; set; }

        public Guid? ArenaId { get; set; }

        public Guid? DeploymentId { get; set; }

        public Guid? BotId { get; set; }

        public DateTime DateTime { get; set; }
    }
}