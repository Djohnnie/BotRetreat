using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BotRetreat.Client.Interfaces;
using BotRetreat.Dashboard.Wpf.Events;
using BotRetreat.DataTransferObjects;
using BotRetreat.Framework.Wpf;
using BotRetreat.Framework.Wpf.Services.Interfaces;
using Reactive.EventAggregator;

namespace BotRetreat.Dashboard.Wpf.ViewModels
{
    public class BotStatisticsViewModel : ViewModelBase
    {
        #region [ Private Members ]

        private readonly IArenaClient _arenaClient;
        private readonly IStatisticsClient _statisticsClient;

        private Team _currentTeam;
        private List<Arena> _availableArenas;
        private Arena _selectedArena;

        #endregion

        #region [ Public Properties ]

        public Team CurrentTeam
        {
            get
            {
                return _currentTeam;
            }
            set
            {
                _currentTeam = value;
                this.NotifyPropertyChanged(x => x.CurrentTeam);
                FetchAvailableArenas();
            }
        }

        public List<Arena> AvailableArenas
        {
            get { return _availableArenas; }
            set
            {
                _availableArenas = value;
                this.NotifyPropertyChanged(x => x.AvailableArenas);
            }
        }

        public Arena SelectedArena
        {
            get { return _selectedArena; }
            set
            {
                _selectedArena = value;
                this.NotifyPropertyChanged(x => x.SelectedArena);
                OnRefresh();
            }
        }

        public ObservableCollection<BotStatistic> BotStatistics { get; } = new ObservableCollection<BotStatistic>();

        #endregion

        #region [ Construction ]

        public BotStatisticsViewModel(IArenaClient arenaClient, IStatisticsClient statisticsClient, ITimerService timerService, IEventAggregator eventAggregator)
        {
            _arenaClient = arenaClient;
            _statisticsClient = statisticsClient;

            InitializeTimer(timerService);
            SubscribeEvents(eventAggregator);
        }

        #endregion

        #region [ Command Handlers ]

        private async void OnRefresh()
        {
            if (CurrentTeam != null && SelectedArena != null)
            {
                await IgnoreExceptions(async () =>
                {
                    var botStatistics = await _statisticsClient.GetBotStatistics(CurrentTeam.Name, CurrentTeam.Password, SelectedArena.Name);
                    BotStatistics.Clear();
                    botStatistics.OrderBy(x => x.PhysicalHealth.Current == 0).ThenBy(x => x.BotName).ToList().ForEach(bs => BotStatistics.Add(bs));
                });
            }
        }

        #endregion

        #region [ Helper Methods ]

        private void InitializeTimer(ITimerService timerService)
        {
            timerService.Start(TimeSpan.FromSeconds(1), OnRefresh);
        }

        private void SubscribeEvents(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<CurrentTeamChangedEvent>().Subscribe(payload => CurrentTeam = payload.Team);
        }

        private async void FetchAvailableArenas()
        {
            AvailableArenas = await _arenaClient.GetTeamArenas(CurrentTeam.Name, CurrentTeam.Password);
            SelectedArena = AvailableArenas.SingleOrDefault(x => x.Name == CurrentTeam.Name);
        }

        #endregion
    }
}