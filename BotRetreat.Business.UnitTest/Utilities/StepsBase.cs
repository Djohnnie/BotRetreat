using System;
using TechTalk.SpecFlow;

namespace BotRetreat.Business.UnitTest.Utilities
{
    public class StepsBase
    {
        public void AddToContext<T>(String key, T value) where T : class
        {
            ScenarioContext.Current.Add<T>(key, value);
        }

        public T GetFromContext<T>(String key) where T : class
        {
            return ScenarioContext.Current.ContainsKey(key) ? ScenarioContext.Current.Get<T>(key) : null;
        }
    }
}