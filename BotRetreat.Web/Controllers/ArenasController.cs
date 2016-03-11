using System;
using System.Collections.Generic;
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
    public class ArenasController : ApiController<IArenaLogic>
    {
        public ArenasController(IArenaLogic arenaLogic) : base(arenaLogic) { }

        [ResponseType(typeof(IEnumerable<Arena>))]
        [HttpGet, Route(RouteConstants.GET_ARENAS)]
        public Task<IHttpActionResult> GetAllArenas()
        {
            return Ok(l => l.GetAllArenas());
        }

        [ResponseType(typeof(IEnumerable<ArenaList>))]
        [HttpGet, Route(RouteConstants.GET_ARENAS_LIST)]
        public Task<IHttpActionResult> GetArenasList()
        {
            return Ok(l => l.GetArenasList());
        }

        [ResponseType(typeof(IEnumerable<Arena>))]
        [HttpGet, Route(RouteConstants.GET_AVAILABLE_ARENAS)]
        public Task<IHttpActionResult> GetAvailableArenas()
        {
            return Ok(l => l.GetAvailableArenas());
        }

        [ResponseType(typeof(Arena))]
        [HttpGet, Route(RouteConstants.GET_TEAM_ARENA)]
        public Task<IHttpActionResult> GetTeamArena(String teamName, String teamPassword)
        {
            return Ok(l => l.GetTeamArena(teamName, teamPassword));
        }

        [ResponseType(typeof(IEnumerable<Arena>))]
        [HttpGet, Route(RouteConstants.GET_TEAM_ARENAS)]
        public Task<IHttpActionResult> GetTeamArenas(String teamName, String teamPassword)
        {
            return Ok(l => l.GetTeamArenas(teamName, teamPassword));
        }

        [ResponseType(typeof(Arena))]
        [HttpPost, Route(RouteConstants.POST_ARENA)]
        public Task<IHttpActionResult> Post(Arena arena)
        {
            return Ok(l => l.CreateArena(arena));
        }

        [ResponseType(typeof(Arena))]
        [HttpPut, Route(RouteConstants.PUT_ARENA)]
        public Task<IHttpActionResult> Put(Arena arena)
        {
            return Ok(l => l.EditArena(arena));
        }

        [ResponseType(typeof(void))]
        [HttpDelete, Route(RouteConstants.DELETE_ARENA)]
        public Task<IHttpActionResult> Delete(Guid id)
        {
            return Ok(l => l.RemoveArena(id));
        }
    }
}