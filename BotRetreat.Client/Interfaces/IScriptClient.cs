using System;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Client.Interfaces
{
    public interface IScriptClient
    {
        Task<ScriptValidation> ValidateScript(String script);
    }
}