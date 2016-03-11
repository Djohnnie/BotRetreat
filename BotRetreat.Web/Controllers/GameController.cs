using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BotRetreat.Business.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Routes;
using BotRetreat.Web.Common;

namespace BotRetreat.Web.Controllers
{
    [RoutePrefix(RouteConstants.PREFIX)]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GameController : ApiController<IGameLogic>
    {
        public GameController(IGameLogic gameLogic) : base(gameLogic) { }

        [ResponseType(typeof(Game))]
        [HttpGet, Route(RouteConstants.GET_GAME)]
        public Task<IHttpActionResult> GetGameForArena(String arenaName)
        {
            return Ok(l => l.GetGameForArena(arenaName));
        }
    }
}