using AutoMapper;
using PositionEntity = BotRetreat.Domain.Position;
using PositionDto = BotRetreat.DataTransferObjects.Position;

namespace BotRetreat.Mappers
{
    public class PositionMapper : MapperBase<PositionEntity, PositionDto>
    {
        public PositionMapper()
        {
            Mapper.CreateMap<PositionEntity, PositionDto>();
            Mapper.CreateMap<PositionDto, PositionEntity>();
        }

        public override PositionDto Map(PositionEntity entity)
        {
            return Mapper.Map<PositionDto>(entity);
        }

        public override PositionEntity Map(PositionDto dataTransferObject)
        {
            return Mapper.Map<PositionEntity>(dataTransferObject);
        }
    }
}