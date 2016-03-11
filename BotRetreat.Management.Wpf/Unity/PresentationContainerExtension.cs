using BotRetreat.Client.Unity;
using Microsoft.Practices.Unity;
using Reactive.EventAggregator;

namespace BotRetreat.Management.Wpf.Unity
{
    public class PresentationContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.AddNewExtension<ClientContainerExtension>();
            Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
        }
    }
}