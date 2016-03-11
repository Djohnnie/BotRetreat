using System;
using System.Data.Entity;
using BotRetreat.Domain;

namespace BotRetreat.DataAccess
{
    public interface IBotRetreatContext : IDbContext
    {

        IDbSet<Arena> Arenas { get; set; }
        IDbSet<Team> Teams { get; set; }
        IDbSet<Deployment> Deployments { get; set; }
        IDbSet<Bot> Bots { get; set; }

    }
}