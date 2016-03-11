using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BotRetreat.Client.Exceptions;
using BotRetreat.Client.Interfaces;
using BotRetreat.Dashboard.Wpf.Events;
using BotRetreat.Dashboard.Wpf.Helpers;
using BotRetreat.DataTransferObjects;
using BotRetreat.Enums;
using BotRetreat.Framework.Wpf;
using BotRetreat.Framework.Wpf.Services.Interfaces;
using BotRetreat.Utilities;
using Reactive.EventAggregator;

namespace BotRetreat.Dashboard.Wpf.ViewModels
{
    public class BotDeploymentViewModel : ViewModelBase
    {
        #region [ Constants ]

        private const String BOT_SCRIPT_FILE_EXPLORER_FILTER = "C# Script (*.cs)|*.cs";

        #endregion

        #region [ Private Members ]

        private readonly IArenaClient _arenaClient;
        private readonly IBotClient _botClient;
        private readonly IDeploymentClient _deploymentClient;
        private readonly IScriptClient _scriptClient;
        private readonly IEventAggregator _eventAggregator;
        private readonly IFileExplorerService _fileExplorerService;
        private readonly IFileService _fileService;

        private Boolean _scriptValid;
        private Team _currentTeam;
        private String _botName;
        private Int16 _botPhysicalHealth;
        private Int16 _botMentalHealth;
        private Int16 _botStamina;
        private Int32 _pointsSpent;
        private String _botScript;
        private List<Arena> _availableArenas;
        private Arena _selectedArena;
        private ScriptValidation _scriptValidation;
        private ScriptValidationMessage _selectedScriptValidationMessage;
        private Point _scriptSelection;

        #endregion

        #region [ Public Properties ]

        public Boolean ScriptValid
        {
            get { return _scriptValid; }
            set
            {
                _scriptValid = value;
                DeployBotCommand.RaiseCanExecuteChanged();
                this.NotifyPropertyChanged(x => x.ScriptValid);
            }
        }

        public Team CurrentTeam
        {
            get { return _currentTeam; }
            set
            {
                _currentTeam = value;
                this.NotifyPropertyChanged(x => x.CurrentTeam);
                FetchAvailableArenas();
            }
        }

        public String BotName
        {
            get { return _botName; }
            set
            {
                _botName = value;
                this.NotifyPropertyChanged(x => x.BotName);
            }
        }

        public Int16 BotPhysicalHealth
        {
            get { return _botPhysicalHealth; }
            set
            {
                _botPhysicalHealth = value;
                DeployBotCommand.RaiseCanExecuteChanged();
                this.NotifyPropertyChanged(x => x.BotPhysicalHealth);
            }
        }

        public Int16 BotStamina
        {
            get { return _botStamina; }
            set
            {
                _botStamina = value;
                DeployBotCommand.RaiseCanExecuteChanged();
                this.NotifyPropertyChanged(x => x.BotStamina);
            }
        }

        public String BotScript
        {
            get { return _botScript; }
            set
            {
                _botScript = value;
                ScriptValid = false;
                this.NotifyPropertyChanged(x => x.BotScript);
            }
        }

        public List<Arena> AvailableArenas
        {
            get { return _availableArenas; }
            set
            {
                _availableArenas = value;
                this.NotifyPropertyChanged(x => x.AvailableArenas);
                this.NotifyPropertyChanged(x => x.TimeUntilNextAllowedDeployment);
                DeployBotCommand.RaiseCanExecuteChanged();
            }
        }

        public Arena SelectedArena
        {
            get { return _selectedArena; }
            set
            {
                _selectedArena = value;
                this.NotifyPropertyChanged(x => x.SelectedArena);
                this.NotifyPropertyChanged(x => x.TimeUntilNextAllowedDeployment);
                DeployBotCommand.RaiseCanExecuteChanged();
            }
        }

        public ScriptValidation ScriptValidation
        {
            get { return _scriptValidation; }
            set
            {
                _scriptValidation = value;
                this.NotifyPropertyChanged(x => x.ScriptValidation);
            }
        }

        public ScriptValidationMessage SelectedScriptValidationMessage
        {
            get { return _selectedScriptValidationMessage; }
            set
            {
                _selectedScriptValidationMessage = value;
                this.NotifyPropertyChanged(x => x.SelectedScriptValidationMessage);
                SelectedScriptValidationMessageChanged();
            }
        }

        public Point ScriptSelection
        {
            get { return _scriptSelection; }
            set
            {
                _scriptSelection = value;
                this.NotifyPropertyChanged(x => x.ScriptSelection);
            }
        }

        public TimeSpan TimeUntilNextAllowedDeployment
        {
            get
            {
                var timeSinceLastDeployment = SelectedArena?.LastDeploymentDateTime != null ? DateTime.UtcNow - SelectedArena.LastDeploymentDateTime.Value : TimeSpan.MaxValue;
                var timeUntilNextAllowedDeployment = (SelectedArena?.DeploymentRestriction.TotalMinutes ?? 0) - timeSinceLastDeployment.TotalMinutes;
                return timeUntilNextAllowedDeployment < 0 ? TimeSpan.Zero : TimeSpan.FromMinutes(timeUntilNextAllowedDeployment);
            }
        }

        public IRelayCommand SaveScriptCommand { get; }
        public IRelayCommand LoadScriptCommand { get; }
        public IRelayCommand ValidateBotScriptCommand { get; }
        public IRelayCommand DeployBotCommand { get; }

        #endregion

        #region [ Construction ]

        public BotDeploymentViewModel(IArenaClient arenaClient, IBotClient botClient, IDeploymentClient deploymentClient, IScriptClient scriptClient, IEventAggregator eventAggregator, IFileExplorerService fileExplorerService, IFileService fileService, ITimerService timerService)
        {
            _arenaClient = arenaClient;
            _botClient = botClient;
            _deploymentClient = deploymentClient;
            _scriptClient = scriptClient;
            _eventAggregator = eventAggregator;
            _fileExplorerService = fileExplorerService;
            _fileService = fileService;

            timerService.Start(TimeSpan.FromSeconds(5), RefreshArenas);

            SubscribeEvents(eventAggregator);

            SaveScriptCommand = new RelayCommand(OnSaveScript);
            LoadScriptCommand = new RelayCommand(OnLoadScript);
            ValidateBotScriptCommand = new RelayCommand(OnValidateBotScript);
            DeployBotCommand = new RelayCommand(OnDeployBot,
                () => SelectedArena != null && ScriptValid && (TimeUntilNextAllowedDeployment <= TimeSpan.Zero && (BotPhysicalHealth + BotStamina <= SelectedArena.MaximumPoints) || CurrentTeam.Predator));
        }

        #endregion

        #region [ Command Handlers ]

        protected async void OnSaveScript()
        {
            await BusyAndExceptionHandling(async () =>
            {
                var fileName = _fileExplorerService.SaveFile(BOT_SCRIPT_FILE_EXPLORER_FILTER);
                await _fileService.SaveTextFile(fileName, BotScript);
            });
        }

        protected async void OnLoadScript()
        {
            await BusyAndExceptionHandling(async () =>
            {
                var fileName = _fileExplorerService.LoadFile(BOT_SCRIPT_FILE_EXPLORER_FILTER);
                BotScript = await _fileService.LoadTextFile(fileName);
            });
        }

        protected async void OnValidateBotScript()
        {
            await BusyAndExceptionHandling(async () =>
            {
                ScriptValidation = await _scriptClient.ValidateScript(BotScript.Base64Encode());
                ScriptValid = ScriptValidation.Messages.Count == 0;
            });
        }

        private async void OnDeployBot()
        {
            await BusyAndExceptionHandling(async () =>
            {
                if (await _deploymentClient.Available(CurrentTeam.Name, SelectedArena.Name))
                {
                    var bot = await _botClient.CreateBot(new Bot
                    {
                        Name = BotName,
                        Script = BotScript.Base64Encode(),
                        Location = new Position { X = 0, Y = 0 },
                        Orientation = Orientation.North,
                        PhysicalHealth = new Health { Maximum = BotPhysicalHealth, Current = BotPhysicalHealth },
                        Stamina = new Health { Maximum = BotStamina, Current = BotStamina },
                        LastAttackLocation = new Position { X = 0, Y = 0 }
                    });
                    if (bot != null)
                    {
                        var deployment = await _deploymentClient.Deploy(new Deployment
                        {
                            ArenaName = SelectedArena.Name,
                            BotName = BotName,
                            TeamName = CurrentTeam.Name
                        });
                        if (deployment != null)
                        {
                            //BotName = "";
                            //BotScript = "";
                            //BotPhysicalHealth = 32000;
                            //BotMentalHealth = 32000;
                            //BotStamina = 32000;
                        }
                    }
                    RefreshArenas();
                }
                else
                {
                    throw new ClientException("New deployment cannot be done. Deployment restriction applies!");
                }
            });
        }

        #endregion

        #region [ Helper Methods ]

        private void SubscribeEvents(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<CurrentTeamChangedEvent>().Subscribe(payload => CurrentTeam = payload.Team);
        }

        private async void FetchAvailableArenas()
        {
            AvailableArenas = await GetTeamArenas();
            SelectedArena = AvailableArenas.SingleOrDefault(x => x.Name == CurrentTeam.Name);
        }

        private async void RefreshArenas()
        {
            if (CurrentTeam != null)
            {
                var selectedArenaId = SelectedArena?.Id;
                AvailableArenas?.Clear();
                AvailableArenas = await GetTeamArenas();
                SelectedArena = AvailableArenas.SingleOrDefault(x => x.Id == selectedArenaId) ??
                                AvailableArenas.SingleOrDefault(x => x.Name == CurrentTeam.Name);
            }
        }

        private async Task<List<Arena>> GetTeamArenas()
        {
            return await _arenaClient.GetTeamArenas(CurrentTeam.Name, CurrentTeam.Password);
        }

        private void SelectedScriptValidationMessageChanged()
        {
            if (SelectedScriptValidationMessage != null)
            {
                ScriptSelection = new Point
                {
                    X = SelectedScriptValidationMessage.LocationStart,
                    Y = SelectedScriptValidationMessage.LocationEnd - SelectedScriptValidationMessage.LocationStart
                };
            }
        }

        private async Task BusyAndExceptionHandling(Func<Task> action)
        {
            using (new IsBusy(_eventAggregator))
            {
                await ExceptionHandling(async () =>
                {
                    await action();
                });
            }
        }

        #endregion
    }
}