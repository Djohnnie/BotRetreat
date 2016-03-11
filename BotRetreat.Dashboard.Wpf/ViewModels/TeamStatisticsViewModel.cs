using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Security;
using System.Windows.Input;
using BotRetreat.Client.Interfaces;
using BotRetreat.Dashboard.Wpf.Events;
using BotRetreat.Dashboard.Wpf.Helpers;
using BotRetreat.DataTransferObjects;
using BotRetreat.Framework.Wpf;
using BotRetreat.Framework.Wpf.Services.Interfaces;
using Reactive.EventAggregator;

namespace BotRetreat.Dashboard.Wpf.ViewModels
{
    public class TeamStatisticsViewModel : ViewModelBase
    {
        #region [ Private Members ]

        private readonly ITeamClient _teamClient;
        private readonly IStatisticsClient _statisticsClient;
        private readonly IEventAggregator _eventAggregator;

        private Team _currentTeam;
        private String _teamName;
        private SecureString _password;

        #endregion

        #region [ Public Properties ]

        public Team CurrentTeam
        {
            get { return _currentTeam; }
            set
            {
                _currentTeam = value;
                _eventAggregator.Publish(new CurrentTeamChangedEvent(_currentTeam));
                this.NotifyPropertyChanged(x => x.CurrentTeam);
            }
        }

        public ObservableCollection<TeamStatistic> TeamStatistics { get; } = new ObservableCollection<TeamStatistic>();

        public String TeamName
        {
            get
            {
                return _teamName;
            }
            set
            {
                _teamName = value;
                AcceptExistingTeamCommand.RaiseCanExecuteChanged();
                CreateNewTeamCommand.RaiseCanExecuteChanged();
            }
        }

        public SecureString TeamPassword
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                AcceptExistingTeamCommand.RaiseCanExecuteChanged();
                CreateNewTeamCommand.RaiseCanExecuteChanged();
            }
        }

        public IRelayCommand AcceptExistingTeamCommand { get; }
        public IRelayCommand CreateNewTeamCommand { get; }

        #endregion

        #region [ Construction ]

        public TeamStatisticsViewModel(ITeamClient teamClient, IStatisticsClient statisticsClient, ITimerService timerService, IEventAggregator eventAggregator)
        {
            _teamClient = teamClient;
            _statisticsClient = statisticsClient;
            timerService.Start(TimeSpan.FromSeconds(1), OnRefresh);
            _eventAggregator = eventAggregator;

            Func<Boolean> canExecute = () =>
            {
                var password = new NetworkCredential(string.Empty, TeamPassword).Password;
                return !String.IsNullOrWhiteSpace(TeamName) && !TeamName.Contains(" ") &&
                       !String.IsNullOrWhiteSpace(password) && !password.Contains(" ");
            };

            AcceptExistingTeamCommand = new RelayCommand(OnAcceptExistingTeam, canExecute);
            CreateNewTeamCommand = new RelayCommand(OnCreateNewTeam, canExecute);
        }

        #endregion

        #region [ Command Handlers ]

        protected async void OnAcceptExistingTeam()
        {
            using (new IsBusy(_eventAggregator))
            {
                await ExceptionHandling(async () =>
                {
                    var password = new NetworkCredential(string.Empty, TeamPassword).Password;
                    CurrentTeam = await _teamClient.GetTeam(TeamName, password);
                });
            }
        }

        private async void OnCreateNewTeam()
        {
            using (new IsBusy(_eventAggregator))
            {
                await ExceptionHandling(async () =>
                {
                    var password = new NetworkCredential(string.Empty, TeamPassword).Password;
                    var team = new Team { Name = TeamName, Password = password };
                    CurrentTeam = await _teamClient.CreateTeam(team);
                });
            }
        }

        private async void OnRefresh()
        {
            if (CurrentTeam != null)
            {
                await IgnoreExceptions(async () =>
                {
                    var teamStatistics = await _statisticsClient.GetTeamStatistics(CurrentTeam.Name, CurrentTeam.Password);
                    TeamStatistics.Clear();
                    teamStatistics.ForEach(ts => TeamStatistics.Add(ts));
                });
            }
        }

        #endregion

    }
}