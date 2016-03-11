using BotRetreat.Client.Unity;
using BotRetreat.Dashboard.Wpf.ViewModels;
using BotRetreat.Framework.Wpf.Services;
using BotRetreat.Framework.Wpf.Services.Interfaces;
using Microsoft.Practices.Unity;
using Reactive.EventAggregator;

namespace BotRetreat.Dashboard.Wpf.Unity
{
    public class PresentationContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.AddNewExtension<ClientContainerExtension>();
            Container.RegisterType<ITimerService, TimerService>(new TransientLifetimeManager());
            Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFileExplorerService, FileExplorerService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFileService, FileService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<MainViewModel>();
            Container.RegisterType<TeamStatisticsViewModel>();
            Container.RegisterType<BotStatisticsViewModel>();
            Container.RegisterType<BotDeploymentViewModel>();
        }
    }
}