using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Dashboard.Wpf.Events
{
    public class CurrentTeamChangedEvent
    {
        public Team Team { get; }

        public CurrentTeamChangedEvent(Team team)
        {
            Team = team;
        }
    }
}