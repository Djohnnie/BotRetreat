using System;
using System.Globalization;
using BotRetreat.Business.UnitTest.Utilities;
using BotRetreat.Domain;
using BotRetreat.Enums;
using BotRetreat.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using HistoryEntity = BotRetreat.Domain.History;
using HistoryDto = BotRetreat.DataTransferObjects.History;

namespace BotRetreat.Business.UnitTest.Steps.Mappers
{
    [Binding]
    public class HistoryMapperSteps : StepsBase
    {
        [Given(@"I have a history entity")]
        public void GivenIHaveAHistoryEntity()
        {
            AddToContext("HistoryEntity", new HistoryEntity());
        }

        [Given(@"The Id property is set to '(.*)'")]
        public void GivenTheIdPropertyIsSetTo(String id)
        {
            GetFromContext<HistoryEntity>("HistoryEntity").Id = Guid.Parse(id);
        }

        [Given(@"The Name property is set to '(.*)'")]
        public void GivenTheNamePropertyIsSetTo(String name)
        {
            GetFromContext<HistoryEntity>("HistoryEntity").Name = name;
        }

        [Given(@"The Description property is set to '(.*)'")]
        public void GivenTheDescriptionPropertyIsSetTo(String description)
        {
            GetFromContext<HistoryEntity>("HistoryEntity").Description = description;
        }

        [Given(@"The Type property is set to '(.*)'")]
        public void GivenTheTypePropertyIsSetTo(String type)
        {
            GetFromContext<HistoryEntity>("HistoryEntity").Type = (HistoryType)Enum.Parse(typeof(HistoryType), type);
        }

        [Given(@"The DateTime property is set to '(.*)'")]
        public void GivenTheDateTimePropertyIsSetTo(String dateTime)
        {
            GetFromContext<HistoryEntity>("HistoryEntity").DateTime = DateTime.ParseExact(dateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        }

        [Given(@"The Arena property is set to an arena with name '(.*)'")]
        public void GivenTheArenaPropertyIsSetToAnArenaWithName(String arenaName)
        {
            //GetFromContext<HistoryEntity>("HistoryEntity").Arena = new Arena { Name = arenaName };
        }

        [Given(@"The Bot property is set to a bot with name '(.*)'")]
        public void GivenTheBotPropertyIsSetToABotWithName(String botName)
        {
            //GetFromContext<HistoryEntity>("HistoryEntity").Deployment = new Deployment { Bot = new Bot { Name = botName } };
        }

        [When(@"I map the entity to a datatransfer object")]
        public void WhenIMapTheEntityToADatatransferObject()
        {
            var historyEntity = GetFromContext<HistoryEntity>("HistoryEntity");
            var historyDto = new HistoryMapper().Map(historyEntity);
            AddToContext("HistoryDto", historyDto);
        }

        [Then(@"the result should be a valid history datatransfer object")]
        public void ThenTheResultShouldBeAValidHistoryDatatransferObject()
        {
            Assert.IsNotNull(GetFromContext<HistoryDto>("HistoryDto"));
        }

        [Then(@"The Id property should be '(.*)'")]
        public void ThenTheIdPropertyShouldBe(String id)
        {
            Assert.AreEqual(Guid.Parse(id), GetFromContext<HistoryDto>("HistoryDto").Id);
        }

        [Then(@"The Name property should be '(.*)'")]
        public void ThenTheNamePropertyShouldBe(String name)
        {
            Assert.AreEqual(name, GetFromContext<HistoryDto>("HistoryDto").Name);
        }

        [Then(@"The Description property should be '(.*)'")]
        public void ThenTheDescriptionPropertyShouldBe(String description)
        {
            Assert.AreEqual(description, GetFromContext<HistoryDto>("HistoryDto").Description);
        }

        [Then(@"The Type property should be '(.*)'")]
        public void ThenTheTypePropertyShouldBe(String type)
        {
            Assert.AreEqual((HistoryType)Enum.Parse(typeof(HistoryType), type), GetFromContext<HistoryDto>("HistoryDto").Type);
        }

        [Then(@"The DateTime property should be '(.*)'")]
        public void ThenTheDateTimePropertyShouldBe(String dateTime)
        {
            Assert.AreEqual(DateTime.ParseExact(dateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture), GetFromContext<HistoryDto>("HistoryDto").DateTime);
        }

        [Then(@"The ArenaName property should be '(.*)'")]
        public void ThenTheArenaNamePropertyShouldBe(String arenaName)
        {
            Assert.AreEqual(arenaName, GetFromContext<HistoryDto>("HistoryDto").ArenaName);
        }

        [Then(@"The BotName property should be '(.*)'")]
        public void ThenTheBotNamePropertyShouldBe(String botName)
        {
            Assert.AreEqual(botName, GetFromContext<HistoryDto>("HistoryDto").BotName);
        }

    }
}