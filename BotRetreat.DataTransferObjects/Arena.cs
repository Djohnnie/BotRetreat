using System;

namespace BotRetreat.DataTransferObjects
{
    public class Arena : IDataTransferObject
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public Boolean Active { get; set; }

        public Int16 Width { get; set; }

        public Int16 Height { get; set; }

        public Boolean Private { get; set; }

        public TimeSpan DeploymentRestriction { get; set; }

        public DateTime LastRefreshDateTime { get; set; }

        public DateTime? LastDeploymentDateTime { get; set; }

        public Int32 MaximumPoints { get; set; }
    }
}