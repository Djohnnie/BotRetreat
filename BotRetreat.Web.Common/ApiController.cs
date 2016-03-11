using System;
using System.Threading.Tasks;
using System.Web.Http;
using BotRetreat.Business.Exceptions;
using BotRetreat.Business.Interfaces;

namespace BotRetreat.Web.Common
{
    public class ApiController<TLogic> : ApiController where TLogic : ILogic
    {
        private readonly TLogic _logic;

        protected ApiController(TLogic logic)
        {
            _logic = logic;
        }

        protected async Task<IHttpActionResult> Ok<TResult>(Func<TLogic, Task<TResult>> logicCall)
        {
            return await Try(async () =>
            {
                var result = await logicCall(_logic);
                return result != null ? Ok(result) : (IHttpActionResult)NotFound();
            });
        }

        protected async Task<IHttpActionResult> Ok(Func<TLogic, Task> logicCall)
        {
            return await Try(async () =>
            {
                await logicCall(_logic);
                return Ok();
            });
        }

        private async Task<IHttpActionResult> Try(Func<Task<IHttpActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}