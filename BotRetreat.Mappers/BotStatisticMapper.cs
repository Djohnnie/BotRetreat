using AutoMapper;
using BotEntity = BotRetreat.Domain.Bot;
using BotStatisticDto = BotRetreat.DataTransferObjects.BotStatistic;
using HealthEntity = BotRetreat.Domain.Health;
using HealthDto = BotRetreat.DataTransferObjects.Health;
using PositionEntity = BotRetreat.Domain.Position;
using PositionDto = BotRetreat.DataTransferObjects.Position;

namespace BotRetreat.Mappers
{
    public class BotStatisticMapper : MapperBase<BotEntity, BotStatisticDto>
    {
        public BotStatisticMapper(IMapper<PositionEntity, PositionDto> positionMapper, IMapper<HealthEntity, HealthDto> healthMapper)
        {
            Mapper.CreateMap<BotEntity, BotStatisticDto>();
            Mapper.CreateMap<BotStatisticDto, BotEntity>();
        }

        public override BotStatisticDto Map(BotEntity entity)
        {
            return Mapper.Map<BotStatisticDto>(entity);
        }

        public override BotEntity Map(BotStatisticDto dataTransferObject)
        {
            return Mapper.Map<BotEntity>(dataTransferObject);
        }
    }
}