using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using BotRetreat.Routes;
using BotRetreat.Web.Common;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;

namespace BotRetreat.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.AddNewExtension<WebContainerExtension>();
            config.DependencyResolver = new UnityResolver(container);

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: RouteConstants.PREFIX + "/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}