using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using BotRetreat.Business.Base;
using BotRetreat.Business.Interfaces;
using BotRetreat.DataAccess;
using BotRetreat.Mappers;
using BotEntity = BotRetreat.Domain.Bot;
using BotDto = BotRetreat.DataTransferObjects.Bot;
using Statistics = BotRetreat.Domain.Statistics;
using BotRetreat.Business.Exceptions;

namespace BotRetreat.Business.Logic
{
    public class BotsLogic : LogicBase<IBotRetreatContext>, IBotsLogic
    {
        private readonly IMapper<BotEntity, BotDto> _botMapper;

        public BotsLogic(IBotRetreatContext dbContext, IMapper<BotEntity, BotDto> botMapper) : base(dbContext)
        {
            _botMapper = botMapper;
        }

        public async Task<List<BotDto>> GetAllBots()
        {
            return _botMapper.Map(await _dbContext.Bots.ToListAsync());
        }

        public async Task<BotDto> CreateBot(BotDto bot)
        {
            var existingBot = await _dbContext.Bots.SingleOrDefaultAsync(x => x.Name == bot.Name);
            if (existingBot != null) { throw new BusinessException($"Bot with name {bot.Name} already exists!"); }
            var botEntity = _botMapper.Map(bot);
            botEntity.Statistics = new Statistics { TimeOfBirth = DateTime.UtcNow };
            _dbContext.Bots.Add(botEntity);
            await _dbContext.SaveChangesAsync();
            return _botMapper.Map(
                await _dbContext.Bots.SingleOrDefaultAsync(x => x.Id == botEntity.Id));
        }

        public async Task<BotDto> EditBot(BotDto bot)
        {
            _dbContext.Bots.Attach(_botMapper.Map(bot));
            await _dbContext.SaveChangesAsync();
            return _botMapper.Map(
                await _dbContext.Bots.SingleOrDefaultAsync(x => x.Id == bot.Id));
        }

        public async Task RemoveBot(Guid botId)
        {
            var existingBot = await _dbContext.Bots.SingleOrDefaultAsync(x => x.Id == botId);
            if (existingBot != null)
            {
                _dbContext.Bots.Remove(existingBot);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}