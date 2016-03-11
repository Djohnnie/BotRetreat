using System;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;
using Microsoft.CodeAnalysis.Scripting;

namespace BotRetreat.Business.Interfaces
{
    public interface IScriptLogic : ILogic
    {
        Task<Script<Object>> PrepareScript(String script);

        Task<ScriptValidation> ValidateScript(String script);
    }
}