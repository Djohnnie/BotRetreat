using System;
using Microsoft.Practices.Unity;

namespace BotRetreat.Dashboard.Wpf.Unity
{
    public class UnityConfiguration
    {
        public static UnityContainer Container { get; } = new Lazy<UnityContainer>(() =>
        {
            var container = new UnityContainer();
            container.AddNewExtension<PresentationContainerExtension>();
            return container;
        }).Value;
    }
}