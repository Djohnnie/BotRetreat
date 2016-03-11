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
    public class TeamsController : ApiController<ITeamsLogic>
    {
        public TeamsController(ITeamsLogic teamsLogic) : base(teamsLogic) { }

        [ResponseType(typeof(IEnumerable<Team>))]
        [HttpGet, Route(RouteConstants.GET_TEAMS)]
        public Task<IHttpActionResult> GetAllTeams()
        {
            return Ok(l => l.GetAllTeams());
        }

        [ResponseType(typeof(Team))]
        [HttpGet, Route(RouteConstants.GET_TEAM)]
        public Task<IHttpActionResult> GetTeam(String name, String password)
        {
            return Ok(l => l.GetTeam(name, password));
        }

        [ResponseType(typeof(Team))]
        [HttpPost, Route(RouteConstants.POST_TEAM)]
        public Task<IHttpActionResult> Post(Team team)
        {
            return Ok(l => l.CreateTeam(team));
        }

        [ResponseType(typeof(Team))]
        [HttpPut, Route(RouteConstants.PUT_TEAM)]
        public Task<IHttpActionResult> Put(Team team)
        {
            return Ok(l => l.EditTeam(team));
        }

        [ResponseType(typeof(void))]
        [HttpDelete, Route(RouteConstants.DELETE_TEAM)]
        public Task<IHttpActionResult> Delete(Guid id)
        {
            return Ok(l => l.RemoveTeam(id));
        }

    }
}