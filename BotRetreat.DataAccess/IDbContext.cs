using System;
using System.Threading.Tasks;

namespace BotRetreat.DataAccess
{
    public interface IDbContext : IDisposable
    {
        Int32 SaveChanges();

        Task<Int32> SaveChangesAsync();
    }
}