using BotRetreat.Arena.Wpf.Services.Design;
using BotRetreat.Framework.Wpf.Services.Design;

namespace BotRetreat.Arena.Wpf.ViewModels.Design
{
    public class DesignMainViewModel : MainViewModel
    {
        public DesignMainViewModel() : base(new DesignHistoryService(), new DesignTimerService()) { }
    }
}