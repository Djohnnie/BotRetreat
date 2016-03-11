using AutoMapper;
using ArenaEntity = BotRetreat.Domain.Arena;
using ArenaListDto = BotRetreat.DataTransferObjects.ArenaList;

namespace BotRetreat.Mappers
{

    public class ArenaListMapper : MapperBase<ArenaEntity, ArenaListDto>
    {
        public ArenaListMapper()
        {
            Mapper.CreateMap<ArenaEntity, ArenaListDto>();
            Mapper.CreateMap<ArenaListDto, ArenaEntity>();
        }

        public override ArenaListDto Map(ArenaEntity entity)
        {
            return Mapper.Map<ArenaListDto>(entity);
        }

        public override ArenaEntity Map(ArenaListDto dataTransferObject)
        {
            return Mapper.Map<ArenaEntity>(dataTransferObject);
        }
    }
}