using Microsoft.Practices.Unity;

namespace BotRetreat.DataAccess.Unity
{
    public class DataAccessContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IBotRetreatContext, BotRetreatContext>(
                new PerResolveLifetimeManager());
            Container.RegisterType<IBotRetreatHistoryContext, BotRetreatHistoryContext>(
                new PerResolveLifetimeManager());
        }
    }
}