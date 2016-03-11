using System.Security;
using BotRetreat.Client.Design;
using BotRetreat.Framework.Wpf.Services.Design;
using Reactive.EventAggregator;

namespace BotRetreat.Dashboard.Wpf.ViewModels.Design
{
    public class DesignTeamStatisticsViewModel : TeamStatisticsViewModel
    {
        public DesignTeamStatisticsViewModel()
            : base(new DesignTeamClient(), new DesignStatisticsClient(), new DesignTimerService(), new EventAggregator())
        {
            TeamName = "De Sjarels";
            TeamPassword = new SecureString();
            OnAcceptExistingTeam();
        }
    }
}