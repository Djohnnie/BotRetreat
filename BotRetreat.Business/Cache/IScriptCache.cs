using System;
using BotRetreat.Domain;
using Script = Microsoft.CodeAnalysis.Scripting.Script;

namespace BotRetreat.Business.Cache
{
    public interface IScriptCache
    {
        Boolean ScriptStored(Bot bot);

        void StoreScript(Bot bot, Script script);

        Script LoadScript(Bot bot);

        void ClearScript(Bot bot);
    }
}