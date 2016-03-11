using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.Client.Base;
using BotRetreat.Client.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Routes;

namespace BotRetreat.Client
{
    public class BotClient : ClientBase, IBotClient
    {
        public Task<List<Bot>> GetAllBots()
        {
            return Get<List<Bot>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_BOTS}");
        }

        public Task<Bot> CreateBot(Bot bot)
        {
            return Post<Bot, Bot>(
              $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_BOT}", bot);
        }

        public Task<Bot> EditBot(Bot bot)
        {
            return Put<Bot, Bot>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.PUT_BOT}", bot);
        }

        public Task RemoveBot(Guid botId)
        {
            return Delete(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.DELETE_BOT}", botId);
        }
    }
}