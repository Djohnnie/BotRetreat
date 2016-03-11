using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.Arena.Wpf.Services.Interfaces;
using BotRetreat.DataTransferObjects;
using RestSharp;

namespace BotRetreat.Arena.Wpf.Services
{
    public class HistoryService : IHistoryService
    {
        public Task<List<History>> GetHistoryByArena(String arenaName, DateTime? fromDateTime = null, DateTime? untilDateTime = null)
        {
            var client = new RestClient("http://localhost:5988/api");
            var resource = $"arena/{arenaName}/history?fromDateTime={fromDateTime}&untilDateTime={untilDateTime}";
            var request = new RestRequest(resource, Method.GET);
            return client.GetTaskAsync<List<History>>(request);
        }

        public Task<List<History>> GetHistoryByBot(String botName, DateTime? fromDateTime = null, DateTime? untilDateTime = null)
        {
            var client = new RestClient("http://localhost:5988/api");
            var resource = $"bot/{botName}/history?fromDateTime={fromDateTime}&untilDateTime={untilDateTime}";
            var request = new RestRequest(resource, Method.GET);
            return client.GetTaskAsync<List<History>>(request);
        }
    }
}