using BotRetreat.Business.Unity;
using Microsoft.Practices.Unity;

namespace BotRetreat.Web.Common
{
    public class WebContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.AddNewExtension<BusinessContainerExtension>();
        }
    }
}