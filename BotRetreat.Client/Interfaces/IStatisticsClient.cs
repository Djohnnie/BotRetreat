using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Client.Interfaces
{
    public interface IStatisticsClient
    {
        Task<List<TeamStatistic>> GetTeamStatistics(String teamName, String teamPassword);

        Task<List<BotStatistic>> GetBotStatistics(String teamName, String teamPassword, String arenaName);
    }
}