using System;
using System.Collections.ObjectModel;
using BotRetreat.Arena.Wpf.Services;
using BotRetreat.Arena.Wpf.Services.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Framework.Wpf.Services;
using BotRetreat.Framework.Wpf.Services.Interfaces;

namespace BotRetreat.Arena.Wpf.ViewModels
{
    public class MainViewModel
    {
        private readonly IHistoryService _historyService;
        private readonly ITimerService _timerService;
        public ObservableCollection<History> History { get; } = new ObservableCollection<History>();

        public MainViewModel() : this(new HistoryService(), new TimerService()) { }

        public MainViewModel(IHistoryService historyService, ITimerService timerService)
        {
            _historyService = historyService;
            _timerService = timerService;
            _timerService.Start(TimeSpan.FromMilliseconds(500), Load);
        }

        private async void Load()
        {
            History.Clear();
            (await _historyService.GetHistoryByArena("arena", DateTime.MinValue, DateTime.MaxValue)).ForEach(history =>
              {
                  History.Add(history);
              });
        }
    }
}