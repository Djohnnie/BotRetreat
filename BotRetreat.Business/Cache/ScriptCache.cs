using System;
using System.Collections.Concurrent;
using BotRetreat.Domain;
using Script = Microsoft.CodeAnalysis.Scripting.Script;

namespace BotRetreat.Business.Cache
{
    public class ScriptCache : IScriptCache
    {
        private readonly ConcurrentDictionary<Guid, Script> _scriptCache = new ConcurrentDictionary<Guid, Script>();

        public Boolean ScriptStored(Bot bot)
        {
            return _scriptCache.ContainsKey(bot.Id);
        }

        public void StoreScript(Bot bot, Script script)
        {
            _scriptCache.AddOrUpdate(bot.Id, script, (a, b) => script);
        }

        public Script LoadScript(Bot bot)
        {
            if (ScriptStored(bot))
            {
                return _scriptCache[bot.Id];
            }
            throw new ArgumentException("No cached script for bot was found.", nameof(bot));
        }

        public void ClearScript(Bot bot)
        {
            if (ScriptStored(bot))
            {
                Script script;
                _scriptCache.TryRemove(bot.Id, out script);
            }
        }
    }
}