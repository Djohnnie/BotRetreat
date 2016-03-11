using System;

namespace BotRetreat.Domain
{
    public class Deployment : IEntity
    {
        public Guid Id { get; set; }

        public virtual Bot Bot { get; set; }

        public virtual Team Team { get; set; }

        public virtual Arena Arena { get; set; }

        public DateTime DeploymentDateTime { get; set; }
    }
}