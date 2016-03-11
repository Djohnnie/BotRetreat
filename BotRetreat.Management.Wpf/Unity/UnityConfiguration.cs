using System;
using BotRetreat.Management.Wpf.ViewModels;
using Microsoft.Practices.Unity;

namespace BotRetreat.Management.Wpf.Unity
{
    public class UnityConfiguration
    {
        public static UnityContainer Container { get; } = new Lazy<UnityContainer>(() =>
        {
            var container = new UnityContainer();
            container.AddNewExtension<PresentationContainerExtension>();
            container.RegisterType<MainViewModel>();
            container.RegisterType<TeamsViewModel>();
            container.RegisterType<ArenasViewModel>();
            return container;
        }).Value;
    }
}