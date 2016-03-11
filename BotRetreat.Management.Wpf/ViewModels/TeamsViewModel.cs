using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BotRetreat.Client.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Framework.Wpf;
using BotRetreat.Management.Wpf.Helpers;
using Reactive.EventAggregator;

namespace BotRetreat.Management.Wpf.ViewModels
{
    public class TeamsViewModel : ViewModelBase
    {
        private readonly ITeamClient _teamClient;
        private readonly IEventAggregator _eventAggregator;

        private TeamStatistic _selectedTeam;

        public ObservableCollection<TeamStatistic> Teams { get; } = new ObservableCollection<TeamStatistic>();

        public TeamStatistic SelectedTeam
        {
            get { return _selectedTeam ?? (_selectedTeam = new TeamStatistic()); }
            set
            {
                _selectedTeam = value;
                this.NotifyPropertyChanged(x => x.SelectedTeam);
            }
        }

        public ICommand CreateTeamCommand { get; }
        public ICommand EditTeamCommand { get; }
        public ICommand RemoveTeamCommand { get; }

        public TeamsViewModel(ITeamClient teamClient, IEventAggregator eventAggregator)
        {
            _teamClient = teamClient;
            _eventAggregator = eventAggregator;

            CreateTeamCommand = new RelayCommand(CreateTeam);
            EditTeamCommand = new RelayCommand(EditTeam);
            RemoveTeamCommand = new RelayCommand(RemoveTeam);
            Load();
        }

        private async Task Load()
        {
            using (new IsBusy(_eventAggregator))
            {
                var teams = await _teamClient.GetAllTeams();
                var arenas = teams.SelectMany(x => x.Statistics).Where(x => x.ArenaName != x.TeamName).ToList();
                Teams.Clear();
                arenas.ForEach(arena => Teams.Add(arena));
            }
        }

        private async void CreateTeam()
        {
            using (new IsBusy(_eventAggregator))
            {
                var team = new Team { Id = SelectedTeam.TeamId, Name = SelectedTeam.TeamName };
                await _teamClient.CreateTeam(team);
                await Load();
            }
        }

        private async void EditTeam()
        {
            using (new IsBusy(_eventAggregator))
            {
                var team = new Team { Id = SelectedTeam.TeamId, Name = SelectedTeam.TeamName };
                await _teamClient.EditTeam(team);
                await Load();
            }
        }

        private async void RemoveTeam()
        {
            using (new IsBusy(_eventAggregator))
            {
                await _teamClient.RemoveTeam(SelectedTeam.TeamId);
                await Load();
            }
        }
    }
}