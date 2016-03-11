using Reactive.EventAggregator;

namespace BotRetreat.Dashboard.Wpf.ViewModels.Design
{
    public class DesignMainViewModel : MainViewModel
    {
        public DesignMainViewModel() : base(new EventAggregator()) { }
    }
}