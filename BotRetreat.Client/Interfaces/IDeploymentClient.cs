using System;
using System.Threading.Tasks;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Client.Interfaces
{
    public interface IDeploymentClient
    {
        Task<Deployment> Deploy(Deployment deployment);

        Task<Boolean> Available(String teamName, String arenaName);
    }
}