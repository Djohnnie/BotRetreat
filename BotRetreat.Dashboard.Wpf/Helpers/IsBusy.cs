using System;
using BotRetreat.Dashboard.Wpf.Events;
using Reactive.EventAggregator;

namespace BotRetreat.Dashboard.Wpf.Helpers
{
    public class IsBusy : IDisposable
    {
        private readonly IEventAggregator _eventAggregator;

        public IsBusy(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Publish(new IsBusyChangedEvent(true));
        }

        public void Dispose()
        {
            _eventAggregator.Publish(new IsBusyChangedEvent(false));
        }
    }
}