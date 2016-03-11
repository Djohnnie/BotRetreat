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
    public class BotsController : ApiController<IBotsLogic>
    {
        public BotsController(IBotsLogic botsLogic) : base(botsLogic) { }

        [ResponseType(typeof(IEnumerable<Bot>))]
        [HttpGet, Route(RouteConstants.GET_BOTS)]
        public Task<IHttpActionResult> GetAllBots()
        {
            return Ok(l => l.GetAllBots());
        }

        [ResponseType(typeof(Bot))]
        [HttpPost, Route(RouteConstants.POST_BOT)]
        public Task<IHttpActionResult> Post(Bot bot)
        {
            return Ok(l => l.CreateBot(bot));
        }

        [ResponseType(typeof(Bot))]
        [HttpPut, Route(RouteConstants.PUT_BOT)]
        public Task<IHttpActionResult> Put(Bot bot)
        {
            return Ok(l => l.EditBot(bot));
        }

        [ResponseType(typeof(void))]
        [HttpDelete, Route(RouteConstants.DELETE_BOT)]
        public Task<IHttpActionResult> Delete(Guid id)
        {
            return Ok(l => l.RemoveBot(id));
        }
    }
}