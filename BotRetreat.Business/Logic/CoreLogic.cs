using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat.Business.Base;
using BotRetreat.Business.Extensions;
using BotRetreat.Business.Interfaces;
using BotRetreat.DataAccess;
using BotRetreat.Domain;
using BotRetreat.Enums;
using System.Data.Entity;
using System.Diagnostics;
using System.Threading;
using BotRetreat.Business.Cache;
using BotRetreat.Utilities;
using Script = Microsoft.CodeAnalysis.Scripting.Script;

namespace BotRetreat.Business.Logic
{
    public class CoreLogic : LogicBase<IBotRetreatContext>, ICoreLogic
    {
        private readonly ILogLogic _logLogic;
        private readonly IScriptLogic _scriptLogic;
        private readonly IScriptCache _scriptCache;
        private readonly Random _randomGenerator = new Random();

        public CoreLogic(IBotRetreatContext dbContext, ILogLogic logLogic, IScriptLogic scriptLogic, IScriptCache scriptCache) : base(dbContext)
        {
            _logLogic = logLogic;
            _scriptLogic = scriptLogic;
            _scriptCache = scriptCache;
        }

        public async Task Go(CancellationToken cancellationToken = default(CancellationToken))
        {
            var sw = Stopwatch.StartNew();

            List<Arena> arenas;
            using (new LoggingStopwatch("Getting arenas from database"))
            {
                // Get all active arenas from the database.
                arenas = await _dbContext.Arenas.Where(x => x.Active).ToListAsync(cancellationToken);
            }

            List<Bot> bots;
            using (new LoggingStopwatch("Getting bots from database"))
            {
                // Get all healthy bots from the database.
                bots = await _dbContext.Bots.Include(b => b.Deployments.Select(d => d.Team)).Where(x => x.PhysicalHealth.Current > 0).ToListAsync(cancellationToken);
                // Randomize bot order
                bots = bots.Select(x => new { Bot = x, Random = _randomGenerator.Next(0, 1000) }).OrderBy(x => x.Random).Select(x => x.Bot).ToList();
            }

            // Get all health statistics from all bots before the iteration.
            var botStats = bots.GetBotStats();

            // Process all active arenas.
            arenas.ForEach(arena => Go(arena, bots, cancellationToken));

            // Update last attack location
            bots.UpdateLastAttackLocation();

            // Update all health statistics from all bots after the iteration.
            bots.UpdateStatDrains(botStats);

            using (new LoggingStopwatch("Saving bots to database"))
            {
                await _dbContext.SaveChangesAsync();
            }

            sw.Stop();
            LogTiming(null, null, $"Worker spent {sw.ElapsedMilliseconds} ms on one cycle.");

            _logLogic.SaveChanges();
        }

        public void Go(Arena arena, List<Bot> bots, CancellationToken cancellationToken = default(CancellationToken))
        {
            var sw = Stopwatch.StartNew();

            List<Bot> activeBots;
            using (new LoggingStopwatch("Searching active bots"))
            {
                activeBots = bots.Where(x => x.Deployments.Any(d => d.Arena.Id == arena.Id)).ToList();
            }

            // Process all active bots in parallel.
            Parallel.ForEach(activeBots, bot => GoInternal(arena, bot, activeBots, cancellationToken).Wait(cancellationToken));

            arena.LastRefreshDateTime = DateTime.UtcNow;

            sw.Stop();
            LogTiming(arena, null, $"Worker spent {sw.ElapsedMilliseconds} ms on arena {arena.Name}.");
        }

        private async Task GoInternal(Arena arena, Bot bot, List<Bot> bots = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Go(arena, bot, bots, cancellationToken);
        }

        public async Task<CoreGlobals> Go(Arena arena, Bot bot, List<Bot> bots = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var botScript = await GetCompiledBotScript(bot);

            CoreGlobals coreGlobals;
            using (new LoggingStopwatch("Initializing Core Globals"))
            {
                // Create globals object from bot properties.
                coreGlobals = new CoreGlobals(arena, bot, bots ?? new List<Bot>(), _logLogic);
            }

            try
            {
                // Log a history entry that the bot script has started to run.
                LogMessage(arena, bot, HistoryName.BotScriptStarted);

                using (new LoggingStopwatch("Running bot script"))
                {
                    // Use CSharp scripting to evaluate the bot script using the globals object.
                    await botScript.RunAsync(coreGlobals, cancellationToken);
                    bot.UpdateBot(coreGlobals);
                }

                // Log a history entry that the bot script has finished running.
                LogMessage(arena, bot, HistoryName.BotScriptFinished);

            }
            catch (Exception ex)
            {
                // Even if there are errors in the botscript, persist memory.
                bot.Memory = coreGlobals.Memory.Serialize();
                // Last action was a script error.
                bot.LastAction = LastAction.ScriptError;
                // If an exception occured when running the bot script, log it as a history entry.
                LogError(arena, bot, HistoryName.BotScriptError, ex);
            }

            // Return the globals object as result of running this script.
            return coreGlobals;
        }

        private async Task<Script> GetCompiledBotScript(Bot bot)
        {
            using (new LoggingStopwatch("Getting bot script from cache"))
            {
                if (!_scriptCache.ScriptStored(bot))
                {
                    var botScript = await _scriptLogic.PrepareScript(bot.Script);
                    botScript.Compile();
                    _scriptCache.StoreScript(bot, botScript);
                }

                return _scriptCache.LoadScript(bot);
            }
        }

        private void LogMessage(Arena arena, Bot bot, HistoryName historyName)
        {
            _logLogic.LogMessage(arena, bot?.Deployments.SingleOrDefault(x => x.Arena.Id == arena.Id), bot, historyName);
        }

        private void LogWarning(Arena arena, Bot bot, HistoryName historyName)
        {
            _logLogic.LogWarning(arena, bot?.Deployments.SingleOrDefault(x => x.Arena.Id == arena.Id), bot, historyName);
        }

        private void LogError(Arena arena, Bot bot, HistoryName historyName, Exception ex)
        {
            _logLogic.LogError(arena, bot?.Deployments.SingleOrDefault(x => x.Arena.Id == arena.Id), bot, historyName, ex.ToString());
        }

        private void LogTiming(Arena arena, Bot bot, String message)
        {
            _logLogic.LogTiming(arena, bot?.Deployments.SingleOrDefault(x => x.Arena.Id == arena.Id), bot, message);
        }
    }
}