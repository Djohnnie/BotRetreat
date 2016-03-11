using BotRetreat.Client.Design;

namespace BotRetreat.Management.Wpf.ViewModels.Design
{
    public class DesignTeamsViewModel : TeamsViewModel
    {
        public DesignTeamsViewModel() : base(new DesignTeamClient(), null) { }
    }
}