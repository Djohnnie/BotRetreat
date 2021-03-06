﻿using System;
using BotRetreat.Dashboard.Wpf.Events;
using BotRetreat.Framework.Wpf;
using Reactive.EventAggregator;

namespace BotRetreat.Dashboard.Wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Boolean _isBusy;

        public Boolean IsBusy
        {
            get
            {
                return _isBusy;

            }
            set
            {
                _isBusy = value;
                this.NotifyPropertyChanged(x => x.IsBusy);
            }
        }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            SubscribeEvents(eventAggregator);
        }

        #region [ Helper Methods ]

        private void SubscribeEvents(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<IsBusyChangedEvent>().Subscribe(payload => IsBusy = payload.IsBusy);
        }

        #endregion
    }
}