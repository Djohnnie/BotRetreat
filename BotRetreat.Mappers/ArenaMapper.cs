using AutoMapper;
using ArenaEntity = BotRetreat.Domain.Arena;
using ArenaDto = BotRetreat.DataTransferObjects.Arena;

namespace BotRetreat.Mappers
{
    public class ArenaMapper : MapperBase<ArenaEntity, ArenaDto>
    {
        public ArenaMapper()
        {
            Mapper.CreateMap<ArenaEntity, ArenaDto>();
            Mapper.CreateMap<ArenaDto, ArenaEntity>();
        }

        public override ArenaDto Map(ArenaEntity entity)
        {
            return Mapper.Map<ArenaDto>(entity);
        }

        public override ArenaEntity Map(ArenaDto dataTransferObject)
        {
            return Mapper.Map<ArenaEntity>(dataTransferObject);
        }
    }
}