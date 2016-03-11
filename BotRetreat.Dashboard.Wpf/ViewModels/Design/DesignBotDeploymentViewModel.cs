using BotRetreat.Client.Design;
using Reactive.EventAggregator;

namespace BotRetreat.Dashboard.Wpf.ViewModels.Design
{
    public class DesignBotDeploymentViewModel : BotDeploymentViewModel
    {
        public DesignBotDeploymentViewModel() : base(new DesignArenaClient(), null, null, null, new EventAggregator(), null, null, null)
        {
            CurrentTeam = new DesignTeamClient().GetTeam("De Sjarels", "").Result;
        }
    }
}