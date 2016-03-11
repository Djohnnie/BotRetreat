using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat.Business.Base;
using BotRetreat.Business.Interfaces;
using BotRetreat.DataAccess;
using BotRetreat.DataTransferObjects;
using BotRetreat.Mappers;
using BotRetreat.Utilities;
using ArenaEntity = BotRetreat.Domain.Arena;
using ArenaDto = BotRetreat.DataTransferObjects.Arena;
using BotEntity = BotRetreat.Domain.Bot;
using BotDto = BotRetreat.DataTransferObjects.Bot;
using HistoryEntity = BotRetreat.Domain.History;
using HistoryDto = BotRetreat.DataTransferObjects.History;

namespace BotRetreat.Business.Logic
{
    public class GameLogic : LogicBase<IBotRetreatContext>, IGameLogic
    {
        private readonly IMapper<ArenaEntity, ArenaDto> _arenaMapper;
        private readonly IMapper<BotEntity, BotDto> _botMapper;
        private readonly IMapper<HistoryEntity, HistoryDto> _historyMapper;

        public GameLogic(IBotRetreatContext dbContext, IMapper<ArenaEntity, ArenaDto> arenaMapper, IMapper<BotEntity, BotDto> botMapper, IMapper<HistoryEntity, HistoryDto> historyMapper) : base(dbContext)
        {
            _arenaMapper = arenaMapper;
            _botMapper = botMapper;
            _historyMapper = historyMapper;
        }

        public async Task<Game> GetGameForArena(String arenaName)
        {
            var arena = await _dbContext.Arenas.SingleOrDefaultAsync(x => x.Name == arenaName);
            var bots = await _dbContext.Deployments
                .Where(x => x.Arena.Name == arenaName)
                .Select(x => x.Bot).OrderByDescending(x => x.Name).ToListAsync();
            bots =
                bots.Where(x => !x.Statistics.TimeOfDeath.HasValue || (DateTime.UtcNow - x.Statistics.TimeOfDeath.Value).TotalMinutes < 2).ToList();
            //var history = await _dbContext.History.Where(x => x.Arena.Name == arenaName).OrderByDescending(x => x.DateTime).ToListAsync();
            bots.ForEach(x =>
            {
                x.Script = String.Empty;
                x.Name = $"{x.Name} ({x.Deployments.Single().Team.Name})";
            });
            return new Game
            {
                Arena = _arenaMapper.Map(arena),
                Bots = _botMapper.Map(bots),
                //History = _historyMapper.Map(history)
            };
        }
    }
}