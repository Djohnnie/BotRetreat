using System;
using TechTalk.SpecFlow;

namespace BotRetreat.Business.UnitTest.Utilities
{
    public static class ScenarioContextExtensions
    {
        public static void Add<T>(this ScenarioContext scenarioContext, String key, T value) where T : class
        {
            scenarioContext.Add(key, value);
        }

        //public static T Get<T>(this ScenarioContext scenarioContext, String key) where T : class
        //{
        //    return scenarioContext.ContainsKey(key) ? scenarioContext[key] as T : null;
        //}
    }
}