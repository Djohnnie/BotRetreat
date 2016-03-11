using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.Client.Base;
using BotRetreat.Client.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Routes;

namespace BotRetreat.Client
{
    public class StatisticsClient : ClientBase, IStatisticsClient
    {
        public Task<List<TeamStatistic>> GetTeamStatistics(String teamName, String teamPassword)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(teamName)] = teamName,
                [nameof(teamPassword)] = teamPassword
            };
            return Get<List<TeamStatistic>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_STATISTICS_TEAM}", parameters);
        }

        public Task<List<BotStatistic>> GetBotStatistics(String teamName, String teamPassword, String arenaName)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(teamName)] = teamName,
                [nameof(teamPassword)] = teamPassword,
                [nameof(arenaName)] = arenaName
            };
            return Get<List<BotStatistic>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_STATISTICS_BOT}", parameters);
        }
    }
}