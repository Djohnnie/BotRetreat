# BotRetreat
Backend and tools for BotRetreatArena

##Project structure
###0. Cloud Services
####BotRetreat
This Microsoft Azure project contains Azure templates for the following deployments:
BotRetreat.Web
BotRetreat.Web.ScriptValidation
BotRetreat.Worker
###1. UI
####BotRetreat.Arena.Wpf
This WPF project should contain a WPF arena client but has not (yet) been completed.
####BotRetreat.Dashboard.Wpf
This WPF project contains a WPF client to manage a team and its bot deployments.
####BotRetreat.Management.Wpf
This WPF project contains a WPF client to manage all teams, arenas, deployment and bots but is barely usable.
####BotRetreat.ScriptEditor.Wpf
This WPF project contains a C# script editor used by the BotRetreat.Dashboard.Wpf project.
####BotRetreat.Framework.Wpf
This WPF project contains all helper classes shared by the other WPF projects.
###2. Client
####BotRetreat.Client
This assembly contains client code for all HTTP webservices used by the different frontends.
####BotRetreat.Client.Design
This assembly contains code to help support WPF XAML design data.
###3. Web
####BotRetreat.Web
This ASP.NET project contains the WebApi HTML webservices used by all frontends.
####BotRetreat.Web.ScriptValidation
This ASP.NET project contains the WebApi HTML webservices used for computational intensive script validation.
####BotRetreat.Web.Common
This assembly contains helper classes shared by the other web projects.
###4. Worker
####BotRetreat.Worker
This assembly contains the Microsoft Azure Worker Role code to process all bots.
###5. Model
####BotRetreat.DataTransferObjects
This assembly contains all DTO classes used by the ASP.NET WebApi projects.
####BotRetreat.Domain
This assembly contains all domain classes used by Entity Framework and the business logic.
####BotRetreat.Enums
This assembly contains all enumeration types shared by the Domain and DataTransferObjects projects.
####BotRetreat.Routes
This assembly contains all HTTP routes shared by the Web and Client projects.
###6. Logic
####BotRetreat.Business
This assembly contains all business logic for the BotRetreat projects.
####BotRetreat.DataAccess
This assembly contains the Entity Framework DbContext classes.
####BotRetreat.Mappers
This assembly contains all the AutoMapper logic to map DTO's to Domain objects and vice versa.
####BotRetreat.Utilities
This assembly contains helper classes shared by all logic related projects.
###7. Testing
####BotRetreat.Business.UnitTest
This assembly contains some unittests written using SpecFlow.
####BotRetreat.Console
This Console Application was used to fiddle around with the code and for testing purposes.
###8. Resources
This folder contains all resources used by the projects in this solution.
###9. Setups
####BotRetreat.Setup
This assembly contains an InnoSetup installation script for the BotRetreat.Dashboard.Wpf client.
