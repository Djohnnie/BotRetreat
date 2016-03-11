using System;
using System.Collections.Generic;

namespace BotRetreat.DataTransferObjects
{
    public class Team : IDataTransferObject
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String Password { get; set; }

        public Boolean Predator { get; set; }

        public List<TeamStatistic> Statistics { get; set; }
    }
}