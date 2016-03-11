using AutoMapper;
using HealthEntity = BotRetreat.Domain.Health;
using HealthDto = BotRetreat.DataTransferObjects.Health;

namespace BotRetreat.Mappers
{
    public class HealthMapper : MapperBase<HealthEntity, HealthDto>
    {
        public HealthMapper()
        {
            Mapper.CreateMap<HealthEntity, HealthDto>();
            Mapper.CreateMap<HealthDto, HealthEntity>();
        }

        public override HealthDto Map(HealthEntity entity)
        {
            return Mapper.Map<HealthDto>(entity);
        }

        public override HealthEntity Map(HealthDto dataTransferObject)
        {
            return Mapper.Map<HealthEntity>(dataTransferObject);
        }
    }
}