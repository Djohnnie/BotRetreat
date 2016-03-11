using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BotRetreat.Client.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Framework.Wpf;

namespace BotRetreat.Management.Wpf.ViewModels
{
    public class BotsViewModel : ViewModelBase
    {
        private readonly IBotClient _botClient;

        private Bot _selectedBot;

        public ObservableCollection<Bot> Bots { get; } = new ObservableCollection<Bot>();

        public Bot SelectedBot
        {
            get { return _selectedBot ?? (_selectedBot = new Bot()); }
            set
            {
                _selectedBot = value;
                this.NotifyPropertyChanged(x => x.SelectedBot);
            }
        }

        public ICommand CreateBotCommand { get; }
        public ICommand EditBotCommand { get; }
        public ICommand RemoveBotCommand { get; }

        public BotsViewModel(IBotClient botClient)
        {
            _botClient = botClient;
            CreateBotCommand = new RelayCommand(CreateBot);
            EditBotCommand = new RelayCommand(EditBot);
            RemoveBotCommand = new RelayCommand(RemoveBot);
            Load();
        }

        private async Task Load()
        {
            var arenas = await _botClient.GetAllBots();
            Bots.Clear();
            arenas.ForEach(team => Bots.Add(team));
        }

        private async void CreateBot()
        {
            await _botClient.CreateBot(SelectedBot);
            await Load();
        }

        private async void EditBot()
        {
            await _botClient.EditBot(SelectedBot);
            await Load();
        }

        private async void RemoveBot()
        {
            await _botClient.RemoveBot(SelectedBot.Id);
            await Load();
        }
    }
}