using AutoMapper;
using TeamEntity = BotRetreat.Domain.Team;
using TeamStatisticDto = BotRetreat.DataTransferObjects.TeamStatistic;

namespace BotRetreat.Mappers
{
    public class TeamStatisticMapper : MapperBase<TeamEntity, TeamStatisticDto>
    {
        public TeamStatisticMapper()
        {
            Mapper.CreateMap<TeamEntity, TeamStatisticDto>();
            Mapper.CreateMap<TeamStatisticDto, TeamEntity>();
        }

        public override TeamStatisticDto Map(TeamEntity entity)
        {
            return Mapper.Map<TeamStatisticDto>(entity);
        }

        public override TeamEntity Map(TeamStatisticDto dataTransferObject)
        {
            return Mapper.Map<TeamEntity>(dataTransferObject);
        }
    }
}