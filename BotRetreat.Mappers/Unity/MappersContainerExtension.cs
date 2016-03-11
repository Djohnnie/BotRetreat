using Microsoft.Practices.Unity;
using TeamEntity = BotRetreat.Domain.Team;
using TeamDto = BotRetreat.DataTransferObjects.Team;
using ArenaEntity = BotRetreat.Domain.Arena;
using ArenaDto = BotRetreat.DataTransferObjects.Arena;
using ArenaListDto = BotRetreat.DataTransferObjects.ArenaList;
using HistoryEntity = BotRetreat.Domain.History;
using HistoryDto = BotRetreat.DataTransferObjects.History;
using BotEntity = BotRetreat.Domain.Bot;
using BotDto = BotRetreat.DataTransferObjects.Bot;
using HealthEntity = BotRetreat.Domain.Health;
using HealthDto = BotRetreat.DataTransferObjects.Health;
using PositionEntity = BotRetreat.Domain.Position;
using PositionDto = BotRetreat.DataTransferObjects.Position;
using TeamStatisticDto = BotRetreat.DataTransferObjects.TeamStatistic;
using BotStatisticDto = BotRetreat.DataTransferObjects.BotStatistic;

namespace BotRetreat.Mappers.Unity
{
    public class MappersContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IMapper<TeamEntity, TeamDto>, TeamMapper>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMapper<ArenaEntity, ArenaDto>, ArenaMapper>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMapper<ArenaEntity, ArenaListDto>, ArenaListMapper>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMapper<HistoryEntity, HistoryDto>, HistoryMapper>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMapper<BotEntity, BotDto>, BotMapper>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMapper<HealthEntity, HealthDto>, HealthMapper>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMapper<PositionEntity, PositionDto>, PositionMapper>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMapper<TeamEntity, TeamStatisticDto>, TeamStatisticMapper>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMapper<BotEntity, BotStatisticDto>, BotStatisticMapper>(new ContainerControlledLifetimeManager());
        }
    }
}