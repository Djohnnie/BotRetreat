using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Business.Interfaces
{
    public interface IStatisticsLogic : ILogic
    {
        Task<List<TeamStatistic>> GetTeamStatistics(String teamName, String teamPassword);

        Task<List<BotStatistic>> GetBotStatistics(String teamName, String teamPassword, String arenaName);
    }
}