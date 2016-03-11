using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BotRetreat.Business.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Routes;
using BotRetreat.Web.Common;

namespace BotRetreat.Web.ScriptValidation.Controllers
{
    [RoutePrefix(RouteConstants.PREFIX)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ScriptsController : ApiController<IScriptLogic>
    {
        public ScriptsController(IScriptLogic scriptLogic) : base(scriptLogic) { }

        [ResponseType(typeof(DataTransferObjects.ScriptValidation))]
        [HttpPost, Route(RouteConstants.POST_SCRIPT_VALIDATION)]
        public Task<IHttpActionResult> ValidateScript([FromBody]String script)
        {
            return Ok(l => l.ValidateScript(script));
        }
    }
}