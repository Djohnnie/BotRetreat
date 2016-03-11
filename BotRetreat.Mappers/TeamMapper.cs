using AutoMapper;
using TeamEntity = BotRetreat.Domain.Team;
using TeamDto = BotRetreat.DataTransferObjects.Team;

namespace BotRetreat.Mappers
{
    public class TeamMapper : MapperBase<TeamEntity, TeamDto>
    {
        public TeamMapper()
        {
            Mapper.CreateMap<TeamEntity, TeamDto>();
            Mapper.CreateMap<TeamDto, TeamEntity>();
        }

        public override TeamDto Map(TeamEntity entity)
        {
            return Mapper.Map<TeamDto>(entity);
        }

        public override TeamEntity Map(TeamDto dataTransferObject)
        {
            return Mapper.Map<TeamEntity>(dataTransferObject);
        }
    }
}