using System;
using System.Threading.Tasks;
using BotRetreat.Client.Base;
using BotRetreat.Client.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Routes;

namespace BotRetreat.Client
{
    public class ScriptClient : ClientBase, IScriptClient
    {
        //public ScriptClient() : base("http://localhost/BotRetreat.Web.ScriptValidation") { }
        public ScriptClient() : base("http://botretreat.cloudapp.net:8080") { }

        public Task<ScriptValidation> ValidateScript(String script)
        {
            return Post<ScriptValidation, String>(
               $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_SCRIPT_VALIDATION}", script);
        }
    }
}