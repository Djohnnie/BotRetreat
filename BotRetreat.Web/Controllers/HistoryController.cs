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
    public class HistoryController : ApiController<IHistoryLogic>
    {
        public HistoryController(IHistoryLogic historyLogic) : base(historyLogic) { }

        [ResponseType(typeof(IEnumerable<History>))]
        [HttpGet, Route(RouteConstants.GET_HISTORY_BY_ARENA)]
        public Task<IHttpActionResult> GetByArena(Guid arenaId, DateTime? fromDateTime = null, DateTime? untilDateTime = null)
        {
            return Ok(l => l.GetHistoryByArenaId(arenaId, fromDateTime, untilDateTime));
        }

        [ResponseType(typeof(IEnumerable<History>))]
        [HttpGet, Route(RouteConstants.GET_HISTORY_BY_BOT)]
        public Task<IHttpActionResult> GetByBot(Guid botId, DateTime? fromDateTime = null, DateTime? untilDateTime = null)
        {
            return Ok(l => l.GetHistoryByBotId(botId, fromDateTime, untilDateTime));
        }
    }
}