using BotRetreat.Management.Wpf.Unity;
using Microsoft.Practices.Unity;

namespace BotRetreat.Management.Wpf.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel
            => UnityConfiguration.Container.Resolve<MainViewModel>();

        public TeamsViewModel TeamsViewModel
            => UnityConfiguration.Container.Resolve<TeamsViewModel>();

        public ArenasViewModel ArenasViewModel
            => UnityConfiguration.Container.Resolve<ArenasViewModel>();
    }
}