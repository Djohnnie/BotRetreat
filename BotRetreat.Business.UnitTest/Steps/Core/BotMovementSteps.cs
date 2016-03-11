using System;
using System.Collections.Generic;
using BotRetreat.Business.Cache;
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
    public class BotMovementSteps : StepsBase
    {

        [Given(@"I have a bot with ""(.*)"" orientation"), Scope(Feature = "BotMovement")]
        public void GivenIHaveABotWithOrientation(String orientation)
        {
            var bot = new Bot
            {
                PhysicalHealth = new Health { Current = 100, Maximum = 100 },
                Stamina = new Health { Current = 100, Maximum = 100 },
                Location = new Position { X = 0, Y = 0 },
                Orientation = (Orientation)Enum.Parse(typeof(Orientation), orientation)
            };
            AddToContext("Bot", bot);
        }

        [Given(@"That bot is deployed on a (.*) x (.*) arena"), Scope(Feature = "BotMovement")]
        public void GivenThatBotIsDeployedOnXxYArena(Int16 width, Int16 height)
        {
            var bot = GetFromContext<Bot>("Bot");
            var team = new Team
            {
                Name = "TestTeam"
            };
            var arena = new Arena
            {
                Width = width,
                Height = height
            };
            var deployment = new Deployment
            {
                Team = team,
                Arena = arena,
                Bot = bot
            };
            team.Deployments = new List<Deployment> { deployment };
            bot.Deployments = new List<Deployment> { deployment };
            AddToContext("Arena", arena);
        }

        [Given(@"That bot is located on (.*) positions from the left and (.*) positions from the top"), Scope(Feature = "BotMovement")]
        public void GivenThatBotIsLocatedOnPositionsFromTheLeftAndPositionsFromTheTop(Int16 xPosition, Int16 yPosition)
        {
            var bot = GetFromContext<Bot>("Bot");
            bot.Location = new Position { X = xPosition, Y = yPosition };
        }

        [When(@"I make the bot move forward"), Scope(Feature = "BotMovement")]
        public void WhenIMakeTheBotMoveForward()
        {
            RunScript("MoveForward();");
        }

        [When(@"I make the bot turn left"), Scope(Feature = "BotMovement")]
        public void WhenIMakeTheBotTurnLeft()
        {
            RunScript("TurnLeft();");
        }

        [When(@"I make the bot turn right"), Scope(Feature = "BotMovement")]
        public void WhenIMakeTheBotTurnRight()
        {
            RunScript("TurnRight();");
        }

        [When(@"I make the bot turn around"), Scope(Feature = "BotMovement")]
        public void WhenIMakeTheBotTurnAround()
        {
            RunScript("TurnAround();");
        }

        [Then(@"The new orientation will be ""(.*)"""), Scope(Feature = "BotMovement")]
        public void ThenTheNewOrientationWillBe(String orientation)
        {
            var coreGlobals = GetFromContext<CoreGlobals>("CoreGlobals");
            Assert.AreEqual((Orientation)Enum.Parse(typeof(Orientation), orientation), coreGlobals.Orientation);
        }

        [Then(@"The orientation will still be ""(.*)"""), Scope(Feature = "BotMovement")]
        public void ThenTheOrientationWillStillBe(String orientation)
        {
            ThenTheNewOrientationWillBe(orientation);
        }

        [Then(@"The bot will be located on (.*) positions from the left and (.*) positions from the top"), Scope(Feature = "BotMovement")]
        public void ThenTheBotWillBeLocatedOnPositionsFromTheLeftAndPositionsFromTheTop(Int16 xPosition, Int16 yPosition)
        {
            var coreGlobals = GetFromContext<CoreGlobals>("CoreGlobals");
            Assert.AreEqual(xPosition, coreGlobals.Location.X);
            Assert.AreEqual(yPosition, coreGlobals.Location.Y);
        }

        private void RunScript(String script)
        {
            var arena = GetFromContext<Arena>("Arena") ?? new Arena();
            var bot = GetFromContext<Bot>("Bot");
            bot.Script = script.Base64Encode();
            var mockContext = MockRepository.GenerateStrictMock<IBotRetreatContext>();
            var mockLogLogic = MockRepository.GenerateStub<ILogLogic>();
            var coreLogic = new CoreLogic(mockContext, mockLogLogic, new ScriptLogic(), new ScriptCache());
            var coreGlobals = coreLogic.Go(arena, bot).Result;
            AddToContext("CoreGlobals", coreGlobals);
        }
    }
}