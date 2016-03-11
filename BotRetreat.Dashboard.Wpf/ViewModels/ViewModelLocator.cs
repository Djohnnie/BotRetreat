using BotRetreat.Dashboard.Wpf.Unity;
using Microsoft.Practices.Unity;

namespace BotRetreat.Dashboard.Wpf.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel
           => UnityConfiguration.Container.Resolve<MainViewModel>();

        public TeamStatisticsViewModel TeamStatisticsViewModel
           => UnityConfiguration.Container.Resolve<TeamStatisticsViewModel>();

        public BotStatisticsViewModel BotStatisticsViewModel
           => UnityConfiguration.Container.Resolve<BotStatisticsViewModel>();

        public BotDeploymentViewModel BotDeploymentViewModel
           => UnityConfiguration.Container.Resolve<BotDeploymentViewModel>();
    }
}