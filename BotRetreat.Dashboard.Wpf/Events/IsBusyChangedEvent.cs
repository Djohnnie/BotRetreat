using System;

namespace BotRetreat.Dashboard.Wpf.Events
{
    public class IsBusyChangedEvent
    {
        public Boolean IsBusy { get; set; }

        public IsBusyChangedEvent(Boolean isBusy)
        {
            IsBusy = isBusy;
        }
    }
}