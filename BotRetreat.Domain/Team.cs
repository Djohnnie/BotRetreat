using System;
using System.Collections.Generic;

namespace BotRetreat.Domain
{
    public class Team : IEntity
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String Password { get; set; }

        public Boolean Predator { get; set; }

        public virtual ICollection<Deployment> Deployments { get; set; }
    }
}