using System;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Business.Interfaces
{
    public interface IDeploymentLogic : ILogic
    {
        Task<Deployment> Deploy(Deployment deployment);

        Task<Boolean> Available(String teamName, String arenaName);
    }
}