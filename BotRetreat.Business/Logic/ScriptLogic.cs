using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using BotRetreat.Business.Interfaces;
using BotRetreat.DataTransferObjects;
using BotRetreat.Enums;
using BotRetreat.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Practices.ObjectBuilder2;
using Rhino.Mocks;

namespace BotRetreat.Business.Logic
{
    public class ScriptLogic : IScriptLogic
    {
        public Task<Script<Object>> PrepareScript(String script)
        {
            return Task.Run(() =>
            {
                var decodedScript = script.Base64Decode();
                var mscorlib = typeof(Object).Assembly;
                var systemCore = typeof(Enumerable).Assembly;
                var botRetreatEnums = typeof(Orientation).Assembly;
                var botRetreatDomain = typeof(Domain.Position).Assembly;
                var botRetreatBusiness = typeof(Business.Interfaces.IBot).Assembly;
                var scriptOptions = ScriptOptions.Default.AddReferences(mscorlib, systemCore, botRetreatEnums, botRetreatDomain, botRetreatBusiness);
                scriptOptions = scriptOptions.WithImports("System", "System.Linq", "System.Collections.Generic", "BotRetreat.Enums", "BotRetreat.Domain", "BotRetreat.Business.Interfaces");
                var botScript = CSharpScript.Create(decodedScript, scriptOptions, typeof(CoreGlobals));
                botScript.WithOptions(botScript.Options.AddReferences(mscorlib, systemCore));
                return botScript;
            });
        }

        public async Task<ScriptValidation> ValidateScript(String script)
        {
            var scriptValidation = new ScriptValidation { Script = script, Messages = new List<ScriptValidationMessage>() };

            var botScript = await PrepareScript(script);

            ImmutableArray<Diagnostic> diagnostics;

            using (var sw = new SimpleStopwatch())
            {
                diagnostics = botScript.Compile();
                scriptValidation.CompilationTimeInMilliseconds = sw.ElapsedMilliseconds;
            }

            if (!diagnostics.Any())
            {
                var task = Task.Run(() =>
                {
                    var arena = new Domain.Arena { Width = 10, Height = 10, Name = "Arena" };
                    var team = new Domain.Team { Name = "MyTeam", Password = "Password" };
                    var enemyTeam = new Domain.Team { Name = "EnemyTeam", Password = "Password" };
                    var bot = new Domain.Bot
                    {
                        Id = Guid.NewGuid(),
                        Name = "Bot",
                        PhysicalHealth = new Domain.Health { Maximum = 100, Current = 100 },
                        Stamina = new Domain.Health { Maximum = 100, Current = 100 },
                        Location = new Domain.Position { X = 1, Y = 1 },
                        Orientation = Orientation.South
                    };
                    var friendBot = new Domain.Bot
                    {
                        Id = Guid.NewGuid(),
                        Name = "Friend",
                        PhysicalHealth = new Domain.Health { Maximum = 100, Current = 100 },
                        Stamina = new Domain.Health { Maximum = 100, Current = 100 },
                        Location = new Domain.Position { X = 1, Y = 3 },
                        Orientation = Orientation.North
                    };
                    var enemyBot = new Domain.Bot
                    {
                        Id = Guid.NewGuid(),
                        Name = "Enemy",
                        PhysicalHealth = new Domain.Health { Maximum = 100, Current = 100 },
                        Stamina = new Domain.Health { Maximum = 100, Current = 100 },
                        Location = new Domain.Position { X = 1, Y = 5 },
                        Orientation = Orientation.North
                    };
                    var deployment = new Domain.Deployment
                    {
                        Arena = arena,
                        Bot = bot,
                        Team = team
                    };
                    var deploymentFriend = new Domain.Deployment
                    {
                        Arena = arena,
                        Bot = friendBot,
                        Team = team
                    };
                    var deploymentEnemy = new Domain.Deployment
                    {
                        Arena = arena,
                        Bot = enemyBot,
                        Team = enemyTeam
                    };
                    team.Deployments = new List<Domain.Deployment> { deployment, deploymentFriend, deploymentEnemy };
                    bot.Deployments = new List<Domain.Deployment> { deployment };
                    friendBot.Deployments = new List<Domain.Deployment> { deploymentFriend };
                    enemyBot.Deployments = new List<Domain.Deployment> { deploymentEnemy };
                    var mockLogLogic = MockRepository.GenerateStub<ILogLogic>();
                    var coreGlobals = new CoreGlobals(arena, bot, new[] { bot, friendBot, enemyBot }.ToList(), mockLogLogic);
                    using (var sw = new SimpleStopwatch())
                    {
                        try
                        {
                            botScript.RunAsync(coreGlobals).Wait();
                        }
                        catch (Exception ex)
                        {
                            scriptValidation.Messages.Add(new ScriptValidationMessage
                            {
                                Message = "Runtime error: " + ex.Message
                            });
                        }
                        scriptValidation.RunTimeInMilliseconds = sw.ElapsedMilliseconds;
                    }
                });

                if (!task.Wait(TimeSpan.FromSeconds(2)))
                {
                    scriptValidation.Messages.Add(new ScriptValidationMessage
                    {
                        Message = "Your script did not finish in a timely fashion!"
                    });
                    scriptValidation.RunTimeInMilliseconds = Int64.MaxValue;
                }
            }

            diagnostics.ForEach(d =>
            {
                if (d.Severity == DiagnosticSeverity.Error)
                {
                    scriptValidation.Messages.Add(new ScriptValidationMessage
                    {
                        Message = d.GetMessage(),
                        LocationStart = d.Location.SourceSpan.Start,
                        LocationEnd = d.Location.SourceSpan.End
                    });
                }
            });

            return scriptValidation;
        }
    }
}