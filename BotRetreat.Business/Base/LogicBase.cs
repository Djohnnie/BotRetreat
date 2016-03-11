using BotRetreat.DataAccess;

namespace BotRetreat.Business.Base
{
    public abstract class LogicBase<TContext> where TContext : IDbContext
    {
        protected readonly TContext _dbContext;

        protected LogicBase(TContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}