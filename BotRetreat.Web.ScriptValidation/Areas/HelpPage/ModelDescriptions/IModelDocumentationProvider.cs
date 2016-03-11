using System;
using System.Reflection;

namespace BotRetreat.Web.ScriptValidation.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}