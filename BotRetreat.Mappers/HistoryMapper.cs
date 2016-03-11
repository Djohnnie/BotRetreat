using AutoMapper;
using HistoryEntity = BotRetreat.Domain.History;
using HistoryDto = BotRetreat.DataTransferObjects.History;

namespace BotRetreat.Mappers
{
    public class HistoryMapper : MapperBase<HistoryEntity, HistoryDto>
    {
        public HistoryMapper()
        {
            Mapper.CreateMap<HistoryEntity, HistoryDto>();
        }

        public override HistoryDto Map(HistoryEntity entity)
        {
            return Mapper.Map<HistoryDto>(entity);
        }

        public override HistoryEntity Map(HistoryDto dataTransferObject)
        {
            return Mapper.Map<HistoryEntity>(dataTransferObject);
        }
    }
}