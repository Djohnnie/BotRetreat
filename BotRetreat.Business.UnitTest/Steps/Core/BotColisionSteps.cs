using System;
using System.Collections.Generic;
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
    public class BotColisionSteps : StepsBase
    {
        [Given(@"I have an (.*) x (.*) arena"), Scope(Feature = "BotColision")]
        public void GivenIHaveAnXArena(Int16 width, Int16 height)
        {
            var arena = new Arena
            {
                Width = width,
                Height = height
            };
            AddToContext("Arena", arena);
        }

        [Given(@"I have a (.*) bot with ""(.*)"" orientation, deployed on (.*) positions from the left and (.*) positions from the top"), Scope(Feature = "BotColision")]
        public void GivenIHaveANumberBotWithOrientationDeployedOnPositionsFromTheLeftAndPositionsFromTheTop(String botNumber, String orientation, Int16 x, Int16 y)
        {
            var arena = GetFromContext<Arena>("Arena");
            var team = new Team
            {
                Name = "TestTeam"
            };
            var bot = new Bot
            {
                Id = Guid.NewGuid(),
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

        [When(@"I make the (.*) bot move forward"), Scope(Feature = "BotColision")]
        public void WhenIMakeTheNumberBotMoveForward(String botNumber)
        {
            var arena = GetFromContext<Arena>("Arena");
            var firstBot = GetFromContext<Bot>("first");
            var secondBot = GetFromContext<Bot>("second");
            var activeBot = GetFromContext<Bot>(botNumber);
            activeBot.Script = "MoveForward();".Base64Encode();
            var mockContext = MockRepository.GenerateStrictMock<IBotRetreatContext>();
            var mockLogLogic = MockRepository.GenerateStub<ILogLogic>();
            var coreLogic = new CoreLogic(mockContext, mockLogLogic, new ScriptLogic(), new ScriptCache());
            var coreGlobals = coreLogic.Go(arena, activeBot, new List<Bot> { firstBot, secondBot }).Result;
            AddToContext("CoreGlobals", coreGlobals);
        }

        [Then(@"The (.*) bot will remain at (.*) positions from the left and (.*) positions from the top"), Scope(Feature = "BotColision")]
        public void ThenTheNumberBotWillRemainAtPositionsFromTheLeftAndPositionsFromTheTop(String botNumber, Int16 x, Int16 y)
        {
            Assert.AreEqual(x, GetFromContext<CoreGlobals>("CoreGlobals").Location.X);
            Assert.AreEqual(y, GetFromContext<CoreGlobals>("CoreGlobals").Location.Y);
        }

        [Then(@"The (.*) bot will remain with a ""(.*)"" orientation"), Scope(Feature = "BotColision")]
        public void ThenTheNumberBotWillRemainWithAOrientation(String botNumber, String orientation)
        {
            Assert.AreEqual((Orientation)Enum.Parse(typeof(Orientation), orientation), GetFromContext<CoreGlobals>("CoreGlobals").Orientation);
        }
    }
}
