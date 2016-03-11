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
    public class DeploymentsController : ApiController<IDeploymentLogic>
    {
        public DeploymentsController(IDeploymentLogic deploymentsLogic) : base(deploymentsLogic) { }

        [ResponseType(typeof(Deployment))]
        [HttpPost, Route(RouteConstants.POST_DEPLOYMENT)]
        public Task<IHttpActionResult> Post(Deployment deployment)
        {
            return Ok(l => l.Deploy(deployment));
        }

        [ResponseType(typeof(Boolean))]
        [HttpGet, Route(RouteConstants.POST_DEPLOYMENT_AVAILABLE)]
        public Task<IHttpActionResult> Get(String teamName, String arenaName)
        {
            return Ok(l => l.Available(teamName, arenaName));
        }
    }
}