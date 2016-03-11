using BotRetreat.Client.Design;
using BotRetreat.Framework.Wpf.Services.Design;
using Reactive.EventAggregator;

namespace BotRetreat.Dashboard.Wpf.ViewModels.Design
{
    public class DesignBotStatisticsViewModel : BotStatisticsViewModel
    {
        public DesignBotStatisticsViewModel() : base(new DesignArenaClient(), new DesignStatisticsClient(), new DesignTimerService(), new EventAggregator())
        {
            CurrentTeam = new DesignTeamClient().GetTeam("De Sjarels", "").Result;
            SelectedArena = new DesignArenaClient().GetTeamArena("De Sjarels", "").Result;
        }
    }
}