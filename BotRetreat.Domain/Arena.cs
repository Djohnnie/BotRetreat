using System;
using System.Collections.Generic;

namespace BotRetreat.Domain
{
    public class Arena : IEntity
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public Boolean Active { get; set; }

        public Int16 Width { get; set; }

        public Int16 Height { get; set; }

        public Boolean Private { get; set; }

        public TimeSpan DeploymentRestriction { get; set; }

        public DateTime LastRefreshDateTime { get; set; }

        public Int32 MaximumPoints { get; set; }

        public virtual ICollection<Deployment> Deployments { get; set; }
    }
}