using System;
using System.Collections.Generic;
using BotRetreat.Enums;

namespace BotRetreat.Domain
{
    public class Bot : IEntity
    {
        public Guid Id { get; set; }

        public Boolean Predator { get; set; }

        public String Name { get; set; }

        public Avatar Avatar { get; set; }

        public Position Location { get; set; }

        public Orientation Orientation { get; set; }

        public Health PhysicalHealth { get; set; }

        public Health Stamina { get; set; }

        public LastAction LastAction { get; set; }

        public Position LastAttackLocation { get; set; }

        public Guid? LastAttackBotId { get; set; }

        public String Script { get; set; }

        public String Memory { get; set; }

        public Statistics Statistics { get; set; }

        public virtual ICollection<Deployment> Deployments { get; set; }
    }
}