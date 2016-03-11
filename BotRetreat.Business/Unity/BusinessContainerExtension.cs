using BotRetreat.Business.Cache;
using BotRetreat.Business.Interfaces;
using BotRetreat.Business.Logic;
using BotRetreat.DataAccess.Unity;
using BotRetreat.Mappers.Unity;
using Microsoft.Practices.Unity;

namespace BotRetreat.Business.Unity
{
    public class BusinessContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.AddNewExtension<DataAccessContainerExtension>();
            Container.AddNewExtension<MappersContainerExtension>();
            Container.RegisterType<IScriptCache, ScriptCache>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ICoreLogic, CoreLogic>(new PerResolveLifetimeManager());
            Container.RegisterType<ITeamsLogic, TeamsLogic>(new PerResolveLifetimeManager());
            Container.RegisterType<IArenaLogic, ArenaLogic>(new PerResolveLifetimeManager());
            Container.RegisterType<ILogLogic, LogLogic>(new PerResolveLifetimeManager());
            Container.RegisterType<IHistoryLogic, HistoryLogic>(new PerResolveLifetimeManager());
            Container.RegisterType<IBotsLogic, BotsLogic>(new PerResolveLifetimeManager());
            Container.RegisterType<IDeploymentLogic, DeploymentLogic>(new PerResolveLifetimeManager());
            Container.RegisterType<IGameLogic, GameLogic>(new PerResolveLifetimeManager());
            Container.RegisterType<IStatisticsLogic, StatisticsLogic>(new PerResolveLifetimeManager());
            Container.RegisterType<IScriptLogic, ScriptLogic>(new PerResolveLifetimeManager());
        }
    }
}