using AutoMapper;
using BotEntity = BotRetreat.Domain.Bot;
using BotDto = BotRetreat.DataTransferObjects.Bot;
using HealthEntity = BotRetreat.Domain.Health;
using HealthDto = BotRetreat.DataTransferObjects.Health;
using PositionEntity = BotRetreat.Domain.Position;
using PositionDto = BotRetreat.DataTransferObjects.Position;

namespace BotRetreat.Mappers
{
    public class BotMapper : MapperBase<BotEntity, BotDto>
    {
        public BotMapper(IMapper<PositionEntity, PositionDto> positionMapper, IMapper<HealthEntity, HealthDto> healthMapper)
        {
            Mapper.CreateMap<BotEntity, BotDto>();
            Mapper.CreateMap<BotDto, BotEntity>();
        }

        public override BotDto Map(BotEntity entity)
        {
            return Mapper.Map<BotDto>(entity);
        }

        public override BotEntity Map(BotDto dataTransferObject)
        {
            return Mapper.Map<BotEntity>(dataTransferObject);
        }
    }
}