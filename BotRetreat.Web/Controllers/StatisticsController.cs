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
    public class StatisticsController : ApiController<IStatisticsLogic>
    {
        public StatisticsController(IStatisticsLogic statisticsLogicLogic) : base(statisticsLogicLogic) { }

        [ResponseType(typeof(IEnumerable<TeamStatistic>))]
        [HttpGet, Route(RouteConstants.GET_STATISTICS_TEAM)]
        public Task<IHttpActionResult> GetTeamStatistics(String teamName, String teamPassword)
        {
            return Ok(l => l.GetTeamStatistics(teamName, teamPassword));
        }

        [ResponseType(typeof(IEnumerable<BotStatistic>))]
        [HttpGet, Route(RouteConstants.GET_STATISTICS_BOT)]
        public Task<IHttpActionResult> GetBotStatistics(String teamName, String teamPassword, String arenaName)
        {
            return Ok(l => l.GetBotStatistics(teamName, teamPassword, arenaName));
        }
    }
}