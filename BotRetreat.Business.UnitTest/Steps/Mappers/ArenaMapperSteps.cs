using System;
using BotRetreat.Business.UnitTest.Utilities;
using BotRetreat.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using ArenaEntity = BotRetreat.Domain.Arena;
using ArenaDto = BotRetreat.DataTransferObjects.Arena;

namespace BotRetreat.Business.UnitTest.Steps.Mappers
{
    [Binding, Scope(Feature = "ArenaMapper")]
    public class ArenaMapperSteps : StepsBase
    {
        [Given(@"I have a arena entity")]
        public void GivenIHaveAArenaEntity()
        {
            AddToContext("ArenaEntity", new ArenaEntity());
        }

        [Given(@"The Id property is set to '(.*)'")]
        public void GivenTheIdPropertyIsSetTo(String id)
        {
            GetFromContext<ArenaEntity>("ArenaEntity").Id = Guid.Parse(id);
        }

        [Given(@"The Name property is set to '(.*)'")]
        public void GivenTheNamePropertyIsSetTo(String name)
        {
            GetFromContext<ArenaEntity>("ArenaEntity").Name = name;
        }

        [Given(@"The Active property is set to '(.*)'")]
        public void GivenTheActivePropertyIsSetTo(String active)
        {
            GetFromContext<ArenaEntity>("ArenaEntity").Active = Boolean.Parse(active);
        }

        [Given(@"The Width property is set to '(.*)'")]
        public void GivenTheWidthPropertyIsSetTo(String width)
        {
            GetFromContext<ArenaEntity>("ArenaEntity").Width = Int16.Parse(width);
        }

        [Given(@"The Height property is set to '(.*)'")]
        public void GivenTheHeightPropertyIsSetTo(String height)
        {
            GetFromContext<ArenaEntity>("ArenaEntity").Height = Int16.Parse(height);
        }

        [Given(@"The Private property is set to '(.*)'")]
        public void GivenThePrivatePropertyIsSetTo(String isPrivate)
        {
            GetFromContext<ArenaEntity>("ArenaEntity").Private = Boolean.Parse(isPrivate);
        }

        [Given(@"The DeploymentRestriction property is set to '(.*)'")]
        public void GivenTheDeploymentRestrictionPropertyIsSetTo(String deploymentRestriction)
        {
            GetFromContext<ArenaEntity>("ArenaEntity").DeploymentRestriction = TimeSpan.FromMinutes(Int32.Parse(deploymentRestriction));
        }

        [When(@"I map the entity to a datatransfer object")]
        public void WhenIMapTheEntityToADatatransferObject()
        {
            var arenaEntity = GetFromContext<ArenaEntity>("ArenaEntity");
            var arenaDto = new ArenaMapper().Map(arenaEntity);
            AddToContext("ArenaDto", arenaDto);
        }

        [Then(@"the result should be a valid arena datatransfer object")]
        public void ThenTheResultShouldBeAValidArenaDatatransferObject()
        {
            Assert.IsNotNull(GetFromContext<ArenaDto>("ArenaDto"));
        }

        [Then(@"The Id property should be '(.*)'")]
        public void ThenTheIdPropertyShouldBe(String id)
        {
            Assert.AreEqual(Guid.Parse(id), GetFromContext<ArenaDto>("ArenaDto").Id);
        }

        [Then(@"The Name property should be '(.*)'")]
        public void ThenTheNamePropertyShouldBe(String name)
        {
            Assert.AreEqual(name, GetFromContext<ArenaDto>("ArenaDto").Name);
        }

        [Then(@"The Active property should be '(.*)'")]
        public void ThenTheActivePropertyShouldBe(String active)
        {
            Assert.AreEqual(Boolean.Parse(active), GetFromContext<ArenaDto>("ArenaDto").Active);
        }

        [Then(@"The Width property should be '(.*)'")]
        public void ThenTheWidthPropertyShouldBe(String width)
        {
            Assert.AreEqual(Int16.Parse(width), GetFromContext<ArenaDto>("ArenaDto").Width);
        }

        [Then(@"The Height property should be '(.*)'")]
        public void ThenTheHeightPropertyShouldBe(String height)
        {
            Assert.AreEqual(Int16.Parse(height), GetFromContext<ArenaDto>("ArenaDto").Height);
        }

        [Then(@"The Private property should be '(.*)'")]
        public void ThenThePrivatePropertyShouldBe(String isPrivate)
        {
            Assert.AreEqual(Boolean.Parse(isPrivate), GetFromContext<ArenaDto>("ArenaDto").Private);
        }

        [Then(@"The DeploymentRestriction property should be '(.*)'")]
        public void ThenTheDeploymentRestrictionPropertyShouldBe(String deploymentRestriction)
        {
            Assert.AreEqual(TimeSpan.FromMinutes(Int32.Parse(deploymentRestriction)), GetFromContext<ArenaDto>("ArenaDto").DeploymentRestriction);
        }
    }
}