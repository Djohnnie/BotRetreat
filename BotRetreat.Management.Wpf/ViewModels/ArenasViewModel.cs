using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BotRetreat.Client.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Framework.Wpf;

namespace BotRetreat.Management.Wpf.ViewModels
{
    public class ArenasViewModel : ViewModelBase
    {
        private readonly IArenaClient _arenaClient;

        private Arena _selectedArena;

        public ObservableCollection<Arena> Arenas { get; } = new ObservableCollection<Arena>();

        public Arena SelectedArena
        {
            get { return _selectedArena ?? (_selectedArena = new Arena()); }
            set
            {
                _selectedArena = value;
                this.NotifyPropertyChanged(x => x.SelectedArena);
            }
        }

        public ICommand CreateArenaCommand { get; }
        public ICommand EditArenaCommand { get; }
        public ICommand RemoveArenaCommand { get; }

        public ArenasViewModel(IArenaClient arenaClient)
        {
            _arenaClient = arenaClient;
            CreateArenaCommand = new RelayCommand(CreateArena);
            EditArenaCommand = new RelayCommand(EditArena);
            RemoveArenaCommand = new RelayCommand(RemoveArena);
            Load();
        }

        private async Task Load()
        {
            var arenas = await _arenaClient.GetAllArenas();
            Arenas.Clear();
            arenas.ForEach(arena => Arenas.Add(arena));
        }

        private async void CreateArena()
        {
            await _arenaClient.CreateArena(SelectedArena);
            await Load();
        }

        private async void EditArena()
        {
            await _arenaClient.EditArena(SelectedArena);
            await Load();
        }

        private async void RemoveArena()
        {
            await _arenaClient.RemoveArena(SelectedArena.Id);
            await Load();
        }
    }
}