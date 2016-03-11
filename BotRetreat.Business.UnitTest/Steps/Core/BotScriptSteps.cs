using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat.Business.Cache;
using BotRetreat.Business.Extensions;
using BotRetreat.Business.Interfaces;
using BotRetreat.Business.Logic;
using BotRetreat.Business.UnitTest.Utilities;
using BotRetreat.DataAccess;
using BotRetreat.Domain;
using BotRetreat.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using TechTalk.SpecFlow;

namespace BotRetreat.Business.UnitTest.Steps.Core
{
    [Binding]
    public class BotScriptSteps : StepsBase
    {
        [Given(@"I have a bot with all health (.*)"), Scope(Feature = "BotScript")]
        public void GivenIHaveABotWithAllHealth(Int16 health)
        {
            var team = new Team
            {
                Name = "TestTeam"
            };
            var arena = new Arena
            {
                Width = 100,
                Height = 100
            };
            AddToContext("Arena", arena);
            var bot = new Bot
            {
                PhysicalHealth = new Health { Current = health, Maximum = health },
                Stamina = new Health { Current = health, Maximum = health },
                Location = new Position { X = 0, Y = 0 }
            };
            var deployment = new Deployment
            {
                Team = team,
                Arena = arena,
                Bot = bot
            };
            team.Deployments = new List<Deployment> { deployment };
            bot.Deployments = new List<Deployment> { deployment };
            AddToContext("Bot", bot);
        }

        [Given(@"A bot script ""(.*)"""), Scope(Feature = "BotScript")]
        public void GivenABotScript(String script)
        {
            GetFromContext<Bot>("Bot").Script = script.Base64Encode();
        }

        [When(@"The bot script runs"), Scope(Feature = "BotScript")]
        public void WhenTheBotScriptRuns()
        {
            var arena = GetFromContext<Arena>("Arena");
            var bot = GetFromContext<Bot>("Bot");
            var mockContext = MockRepository.GenerateStrictMock<IBotRetreatContext>();
            var mockLogLogic = MockRepository.GenerateStub<ILogLogic>();
            var coreLogic = new CoreLogic(mockContext, mockLogLogic, new ScriptLogic(), new ScriptCache());
            var coreGlobals = coreLogic.Go(arena, bot).Result;
            AddToContext("CoreGlobals", coreGlobals);
        }

        [Then(@"The X position member will not have changed"), Scope(Feature = "BotScript")]
        public void ThenTheXPositionMemberWillNotHaveChanged()
        {
            Assert.AreEqual(0, GetFromContext<CoreGlobals>("CoreGlobals").Location.X);
        }

        [Then(@"The X position property will not have changed"), Scope(Feature = "BotScript")]
        public void ThenTheXPositionPropertyWillNotHaveChanged()
        {
            Assert.AreEqual(0, GetFromContext<CoreGlobals>("CoreGlobals").Location.X);
        }

        [Then(@"The Y position member will not have changed"), Scope(Feature = "BotScript")]
        public void ThenTheYPositionMemberWillNotHaveChanged()
        {
            Assert.AreEqual(0, GetFromContext<CoreGlobals>("CoreGlobals").Location.Y);
        }

        [Then(@"The Y position property will not have changed"), Scope(Feature = "BotScript")]
        public void ThenTheYPositionPropertyWillNotHaveChanged()
        {
            Assert.AreEqual(0, GetFromContext<CoreGlobals>("CoreGlobals").Location.Y);
        }

    }
}