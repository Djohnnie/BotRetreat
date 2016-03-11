using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat.Business.Base;
using BotRetreat.Business.Interfaces;
using BotRetreat.DataAccess;
using BotRetreat.Mappers;
using HistoryEntity = BotRetreat.Domain.History;
using HistoryDto = BotRetreat.DataTransferObjects.History;

namespace BotRetreat.Business.Logic
{
    public class HistoryLogic : LogicBase<IBotRetreatHistoryContext>, IHistoryLogic
    {
        private readonly IMapper<HistoryEntity, HistoryDto> _historyMapper;

        public HistoryLogic(IBotRetreatHistoryContext dbContext, IMapper<HistoryEntity, HistoryDto> historyMapper) : base(dbContext)
        {
            _historyMapper = historyMapper;
        }

        public async Task<List<HistoryDto>> GetHistoryByArenaId(Guid arenaId, DateTime? fromDateTime = null, DateTime? untilDateTime = null)
        {
            var historyQuery = _dbContext.History.Where(x => x.ArenaId == arenaId);
            historyQuery = WhereQuery(historyQuery, fromDateTime, untilDateTime);
            return _historyMapper.Map(await historyQuery.ToListAsync());
        }

        public async Task<List<HistoryDto>> GetHistoryByBotId(Guid botId, DateTime? fromDateTime = null, DateTime? untilDateTime = null)
        {
            var historyQuery = _dbContext.History.Where(x => x.BotId == botId);
            historyQuery = WhereQuery(historyQuery, fromDateTime, untilDateTime);
            return _historyMapper.Map(await historyQuery.ToListAsync());
        }

        private static IQueryable<HistoryEntity> WhereQuery(IQueryable<HistoryEntity> historyQuery, DateTime? fromDateTime, DateTime? untilDateTime)
        {
            if (fromDateTime.HasValue)
            {
                historyQuery = historyQuery.Where(x => x.DateTime >= fromDateTime.Value);
            }
            if (untilDateTime.HasValue)
            {
                historyQuery = historyQuery.Where(x => x.DateTime <= untilDateTime.Value);
            }
            return historyQuery;
        }
    }
}