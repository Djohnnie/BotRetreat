using System;
using System.Collections.Generic;
using System.Linq;
using BotRetreat.Business.Logic;
using BotRetreat.Domain;
using BotRetreat.Enums;
using Microsoft.Practices.ObjectBuilder2;

namespace BotRetreat.Business.Extensions
{
    public static class BotExtensions
    {
        public static void UpdateBot(this Bot bot, CoreGlobals coreGlobals)
        {
            bot.Location.X = coreGlobals.Location.X;
            bot.Location.Y = coreGlobals.Location.Y;
            bot.Orientation = coreGlobals.Orientation;
            bot.LastAction = coreGlobals.CurrentAction;
            bot.PhysicalHealth.Current = coreGlobals.PhysicalHealth;
            bot.Stamina.Current = coreGlobals.Stamina;
            bot.LastAttackLocation = coreGlobals.LastAttackLocation;
            bot.LastAttackBotId = coreGlobals.LastAttackBotId;
            bot.Statistics.PhysicalDamageDone += coreGlobals.PhysicalDamageDone;
            bot.Statistics.Kills += coreGlobals.Kills;
            if (bot.LastAction == LastAction.SelfDestruct)
            {
                bot.Statistics.TimeOfDeath = DateTime.UtcNow;
            }
            bot.Memory = coreGlobals.Memory.Serialize();
        }

        public static IEnumerable<BotStat> GetBotStats(this IEnumerable<Bot> bots)
        {
            return bots.Select(bot => new BotStat
            {
                BotId = bot.Id,
                PhysicalHealth = bot.PhysicalHealth.Current,
                Stamina = bot.Stamina.Current
            }).ToList();
        }

        public static void UpdateStatDrains(this IEnumerable<Bot> bots, IEnumerable<BotStat> botStats)
        {
            bots.ForEach(bot =>
            {
                var botStat = botStats.Single(s => s.BotId == bot.Id);
                bot.PhysicalHealth.Drain = (Int16)(botStat.PhysicalHealth - bot.PhysicalHealth.Current);
                bot.Stamina.Drain = (Int16)(botStat.Stamina - bot.Stamina.Current);
            });
        }

        public static void UpdateLastAttackLocation(this IEnumerable<Bot> bots)
        {
            var botList = bots.ToList();
            botList.ForEach(bot =>
            {
                if (bot.LastAttackBotId.HasValue)
                {
                    var attackedBot = botList.SingleOrDefault(x => x.Id == bot.LastAttackBotId.Value);
                    if (attackedBot != null)
                    {
                        bot.LastAttackLocation = new Position { X = attackedBot.Location.X, Y = attackedBot.Location.Y };
                    }
                }
            });
        }
    }
}