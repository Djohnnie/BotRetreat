using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.Client.Base;
using BotRetreat.Client.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Routes;

namespace BotRetreat.Client
{
    public class DeploymentClient : ClientBase, IDeploymentClient
    {
        public Task<Deployment> Deploy(Deployment deployment)
        {
            return Post<Deployment, Deployment>(
               $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_DEPLOYMENT}", deployment);
        }

        public Task<Boolean> Available(String teamName, String arenaName)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(teamName)] = teamName,
                [nameof(arenaName)] = arenaName
            };
            return Get<Boolean>(
              $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_DEPLOYMENT_AVAILABLE}", parameters);
        }
    }
}