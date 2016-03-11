using System;
using System.Data.Entity;
using System.Threading.Tasks;
using BotRetreat.Domain;

namespace BotRetreat.DataAccess
{
    public interface IBotRetreatHistoryContext : IDbContext
    {
        IDbSet<History> History { get; set; }

    }
}