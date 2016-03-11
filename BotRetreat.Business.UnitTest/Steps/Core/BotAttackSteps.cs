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
    public class BotAttackSteps : StepsBase
    {
        [Given(@"I have a bot with ""(.*)"" orientation"), Scope(Feature = "BotAttack")]
        public void GivenIHaveABotWithOrientation(String orientation)
        {
            var bot = new Bot
            {
                PhysicalHealth = new Health { Current = 100, Maximum = 100 },
                Stamina = new Health { Current = 100, Maximum = 100 },
                Orientation = (Orientation)Enum.Parse(typeof(Orientation), orientation)
            };
            AddToContext("Bot", bot);
        }

        [Given(@"That bot is deployed on a (.*) x (.*) arena"), Scope(Feature = "BotAttack")]
        public void GivenThatBotIsDeployedOnAXArena(Int16 width, Int16 height)
        {
            var bot = GetFromContext<Bot>("Bot");
            var team = new Team
            {
                Name = "TestTeam"
            };
            AddToContext("Team", team);
            var arena = new Arena
            {
                Width = width,
                Height = height
            };
            AddToContext("Arena", arena);
            var deployment = new Deployment
            {
                Team = team,
                Arena = arena,
                Bot = bot
            };
            team.Deployments = new List<Deployment> { deployment };
            bot.Deployments = new List<Deployment> { deployment };
        }

        [Given(@"That bot is located on (.*) positions from the left and (.*) positions from the top"), Scope(Feature = "BotAttack")]
        public void GivenThatBotIsLocatedOnPositionsFromTheLeftAndPositionsFromTheTop(Int16 xPosition, Int16 yPosition)
        {
            var bot = GetFromContext<Bot>("Bot");
            bot.Location = new Position { X = xPosition, Y = yPosition };
        }

        [Given(@"A bot with physical health of (.*) is deployed on (.*) positions from the left and (.*) positions from the top"), Scope(Feature = "BotAttack")]
        public void GivenABotWithPhysicalHealthOfIsDeployedOnPositionsFromTheLeftAndPositionsFromTheTop(Int16 physicalHealth, Int16 x, Int16 y)
        {
            var bot = new Bot
            {
                PhysicalHealth = new Health { Current = physicalHealth, Maximum = physicalHealth },
                Stamina = new Health { Current = 100, Maximum = 100 },
                Orientation = Orientation.North,
                Location = new Position { X = x, Y = y }
            };
            AddToContext($"Bot{x}{y}", bot);
            var team = GetFromContext<Team>("Team");
            var arena = GetFromContext<Arena>("Arena");
            var deployment = new Deployment
            {
                Team = team,
                Arena = arena,
                Bot = bot
            };
            team.Deployments = new List<Deployment> { deployment };
            bot.Deployments = new List<Deployment> { deployment };
        }

        [When(@"I make the bot self destruct"), Scope(Feature = "BotAttack")]
        public void WhenIMakeTheBotSelfDestruct()
        {
            var arena = GetFromContext<Arena>("Arena") ?? new Arena();
            var bot = GetFromContext<Bot>("Bot");
            bot.Script = "SelfDestruct();".Base64Encode();
            var mockContext = MockRepository.GenerateStrictMock<IBotRetreatContext>();
            var mockLogLogic = MockRepository.GenerateStub<ILogLogic>();
            var coreLogic = new CoreLogic(mockContext, mockLogLogic, new ScriptLogic(), new ScriptCache());
            var bots = new List<Bot>();
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    if (!(x == 1 && y == 1))
                    {
                        bots.Add(GetFromContext<Bot>($"Bot{x}{y}"));
                    }
                }
            }
            var coreGlobals = coreLogic.Go(arena, bot, bots).Result;
            AddToContext("CoreGlobals", coreGlobals);
        }

        [Then(@"The self destructing bot will be dead"), Scope(Feature = "BotAttack")]
        public void ThenTheSelfDestructingBotWillBeDead()
        {
            var bot = GetFromContext<Bot>("Bot");
            Assert.AreEqual(0, bot.PhysicalHealth.Current);
        }

        [Then(@"The bot located on (.*) positions from the left and (.*) positions from the top will have a physical health of (.*)"), Scope(Feature = "BotAttack")]
        public void ThenTheBotLocatedOnPositionsFromTheLeftAndPositionsFromTheTopWillHaveAPhysicalHealthOfX(Int16 x, Int16 y, Int16 physicalHealth)
        {
            var bot = GetFromContext<Bot>($"Bot{x}{y}");
            Assert.AreEqual(physicalHealth, bot.PhysicalHealth.Current);
        }
    }
}