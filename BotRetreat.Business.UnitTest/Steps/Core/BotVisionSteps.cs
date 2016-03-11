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
using BotRetreat.Enums;
using BotRetreat.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using TechTalk.SpecFlow;

namespace BotRetreat.Business.UnitTest.Steps.Core
{
    [Binding]
    public class BotVisionSteps : StepsBase
    {
        [Given(@"I have an (.*) x (.*) arena"), Scope(Feature = "BotVision")]
        public void GivenIHaveAnXArena(Int16 width, Int16 height)
        {
            var arena = new Arena
            {
                Width = width,
                Height = height
            };
            AddToContext("Arena", arena);
            var team = new Team
            {
                Name = "TestTeam"
            };
            AddToContext("Team", team);
        }

        [Given(@"I have a (.*) bot with ""(.*)"" orientation, deployed on (.*) positions from the left and (.*) positions from the top")]
        public void GivenIHaveANumberBotWithOrientationDeployedOnPositionsFromTheLeftAndPositionsFromTheTop(String botNumber, String orientation, Int16 x, Int16 y)
        {
            var arena = GetFromContext<Arena>("Arena");
            var team = GetFromContext<Team>("Team");
            var bot = new Bot
            {
                Id = Guid.NewGuid(),
                Name = botNumber,
                PhysicalHealth = new Health { Current = 100, Maximum = 100 },
                Stamina = new Health { Current = 100, Maximum = 100 },
                Location = new Position { X = x, Y = y },
                Orientation = (Orientation)Enum.Parse(typeof(Orientation), orientation)
            };
            var deployment = new Deployment
            {
                Team = team,
                Arena = arena,
                Bot = bot
            };
            team.Deployments = new List<Deployment> { deployment };
            bot.Deployments = new List<Deployment> { deployment };
            AddToContext(botNumber, bot);
        }

        [When(@"I investigate the vision of the (.*) bot")]
        public void WhenIInvestigateTheVisionOfTheNumberBot(String botNumber)
        {
            var arena = GetFromContext<Arena>("Arena");
            var firstBot = GetFromContext<Bot>("first");
            var secondBot = GetFromContext<Bot>("second");
            var thirdBot = GetFromContext<Bot>("third");
            var activeBot = GetFromContext<Bot>(botNumber);
            activeBot.Script = "".Base64Encode();
            var mockContext = MockRepository.GenerateStrictMock<IBotRetreatContext>();
            var mockLogLogic = MockRepository.GenerateStub<ILogLogic>();
            var coreLogic = new CoreLogic(mockContext, mockLogLogic, new ScriptLogic(), new ScriptCache());
            var coreGlobals = coreLogic.Go(arena, activeBot, new List<Bot> { firstBot, secondBot, thirdBot }).Result;
            AddToContext("CoreGlobals", coreGlobals);
        }

        [Then(@"The (.*) bot will be visible on (.*) positions from the left and (.*) positions from the top")]
        public void ThenTheNumberBotWillBeVisibleOnPositionsFromTheLeftAndPositionsFromTheTop(String botNumber, Int16 x, Int16 y)
        {
            var actualBot = GetFromContext<Bot>(botNumber);
            var coreGlobals = GetFromContext<CoreGlobals>("CoreGlobals");
            Assert.AreEqual(1, coreGlobals.Vision.FriendlyBots.Count);
            Assert.AreEqual(x, coreGlobals.Vision.FriendlyBots[0].Location.X);
            Assert.AreEqual(y, coreGlobals.Vision.FriendlyBots[0].Location.Y);
            Assert.AreEqual(actualBot.Name, coreGlobals.Vision.FriendlyBots[0].Name);
        }

        [Then(@"The (.*) bot will not be visible")]
        public void ThenTheNumberBotWillNotBeVisible(String botNumber)
        {
            var actualBot = GetFromContext<Bot>(botNumber);
            var coreGlobals = GetFromContext<CoreGlobals>("CoreGlobals");
            Assert.AreNotEqual(actualBot.Name, coreGlobals.Vision.FriendlyBots[0].Name);
        }
    }
}
