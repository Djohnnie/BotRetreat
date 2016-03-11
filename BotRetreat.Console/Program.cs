using System;
using System.Threading.Tasks;
using BotRetreat.Business.Logic;
using BotRetreat.Client;
using BotRetreat.DataTransferObjects;
using BotRetreat.Enums;
using BotRetreat.Utilities;

namespace BotRetreat.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialCreate().Wait();
            //AddColidingBots().Wait();
            //AddEvadingBots().Wait();
            //AddFightingBot().Wait();
            //System.Console.ReadKey();
            var script =
                "bool aboutToColideWithEnemyBot = false; foreach (var visibleBot in Vision.FriendlyBots){ switch (Orientation){case NORTH:if (visibleBot.Location.X == Location.X && visibleBot.Location.Y == Location.Y - 1){aboutToColideWithEnemyBot = true;}break;case EAST:if (visibleBot.Location.X == Location.X + 1 && visibleBot.Location.Y == Location.Y){aboutToColideWithEnemyBot = true;}break;case SOUTH:if (visibleBot.Location.X == Location.X && visibleBot.Location.Y == Location.Y + 1){aboutToColideWithEnemyBot = true;}break;case WEST:if (visibleBot.Location.X == Location.X - 1 && visibleBot.Location.Y == Location.Y){aboutToColideWithEnemyBot = true;}break;}}if ((Orientation == WEST && Location.X == 0) || (Orientation == EAST && Location.X == Width - 1) || (Orientation == NORTH && Location.Y == 0) || (Orientation == SOUTH && Location.Y == Height - 1)){TurnAround();}else{if (aboutToColideWithEnemyBot){TurnLeft();}else{MoveForward();}}"
                    .Base64Encode();
            var result = new ScriptLogic().ValidateScript(script).Result;
            System.Console.ReadKey();
        }

        static async Task InitialCreate()
        {
            await CreateArena();
            //await CreateTeam();
            //await CreateBots();
            //await DeployBots();
        }

        static async Task AddColidingBots()
        {
            var bot1 = new Bot
            {
                Name = "SjarelB1",
                Avatar = Avatar.BeigeDread,
                PhysicalHealth = new Health { Maximum = 400, Current = 400 },
                Stamina = new Health { Maximum = 300, Current = 300 },
                Location = new Position { X = 0, Y = 0 },
                Orientation = Orientation.East,
                Script = "MoveForward();".Base64Encode()
            };
            await new BotClient().CreateBot(bot1);
            var bot2 = new Bot
            {
                Name = "SjarelB2",
                Avatar = Avatar.BeigeDread,
                PhysicalHealth = new Health { Maximum = 400, Current = 400 },
                Stamina = new Health { Maximum = 300, Current = 300 },
                Location = new Position { X = 19, Y = 0 },
                Orientation = Orientation.West,
                Script = "MoveForward();".Base64Encode()
            };
            await new BotClient().CreateBot(bot2);
            var deployment1 = new Deployment
            {
                ArenaName = "DeathMatchArena",
                BotName = "SjarelB1",
                TeamName = "De Sjarels"
            };
            await new DeploymentClient().Deploy(deployment1);
            var deployment2 = new Deployment
            {
                ArenaName = "DeathMatchArena",
                BotName = "SjarelB2",
                TeamName = "De Sjarels"
            };
            await new DeploymentClient().Deploy(deployment2);
        }

        static async Task AddEvadingBots()
        {
            var r = new Random();
            var script =
                "bool aboutToColideWithEnemyBot = false; foreach (var visibleBot in Vision.FriendlyBots){ switch (Orientation){case NORTH:if (visibleBot.Location.X == Location.X && visibleBot.Location.Y == Location.Y - 1){aboutToColideWithEnemyBot = true;}break;case EAST:if (visibleBot.Location.X == Location.X + 1 && visibleBot.Location.Y == Location.Y){aboutToColideWithEnemyBot = true;}break;case SOUTH:if (visibleBot.Location.X == Location.X && visibleBot.Location.Y == Location.Y + 1){aboutToColideWithEnemyBot = true;}break;case WEST:if (visibleBot.Location.X == Location.X - 1 && visibleBot.Location.Y == Location.Y){aboutToColideWithEnemyBot = true;}break;}}if ((Orientation == WEST && Location.X == 0) || (Orientation == EAST && Location.X == Width - 1) || (Orientation == NORTH && Location.Y == 0) || (Orientation == SOUTH && Location.Y == Height - 1)){TurnAround();}else{if (aboutToColideWithEnemyBot){TurnLeft();}else{MoveForward();}}"
                    .Base64Encode();
            for (int i = 0; i < 10; i++)
            {
                var bot = new Bot
                {
                    Name = "SjarelZ" + i,
                    Avatar = Avatar.BeigeDread,
                    PhysicalHealth = new Health { Maximum = 400, Current = 400 },
                    Stamina = new Health { Maximum = 32000, Current = 32000 },
                    Location = new Position { X = (Int16)r.Next(20), Y = (Int16)r.Next(20) },
                    Orientation = (Orientation)r.Next(4),
                    Script = script
                };
                await new BotClient().CreateBot(bot);
                var deployment = new Deployment
                {
                    ArenaName = "DeathMatchArena",
                    BotName = "SjarelZ" + i,
                    TeamName = "De Sjarels"
                };
                await new DeploymentClient().Deploy(deployment);
            }
        }

        static async Task AddFightingBot()
        {
            var r = new Random();
            var script =
                "var enemyBot = Vision.Bots.First(); RangedAttack(enemyBot.Location.X, enemyBot.Location.Y);"
                    .Base64Encode();
            var bot = new Bot
            {
                Name = "SjarelRanged",
                Avatar = Avatar.BeigeDread,
                PhysicalHealth = new Health { Maximum = 400, Current = 400 },
                Stamina = new Health { Maximum = 32000, Current = 32000 },
                Location = new Position { X = (Int16)r.Next(20), Y = (Int16)r.Next(20) },
                Orientation = (Orientation)r.Next(4),
                Script = script
            };
            await new BotClient().CreateBot(bot);
            var deployment = new Deployment
            {
                ArenaName = "DeathMatchArena",
                BotName = "SjarelRanged",
                TeamName = "De Sjarels"
            };
            await new DeploymentClient().Deploy(deployment);
        }

        static async Task CreateArena()
        {
            var arena = new Arena
            {
                Name = "DeathMatchArena",
                Active = true,
                Private = false,
                Width = 20,
                Height = 20,
                DeploymentRestriction = TimeSpan.FromMinutes(10),
                LastRefreshDateTime = DateTime.UtcNow
            };
            await new ArenaClient().CreateArena(arena);
        }

        static async Task CreateTeam()
        {
            var team = new Team
            {
                Name = "De Sjarels",
                Password = "tetten"
            };
            await new TeamClient().CreateTeam(team);
        }

        static async Task CreateBots()
        {
            var bot1 = new Bot
            {
                Name = "SjarelA1",
                Avatar = Avatar.BeigeDread,
                PhysicalHealth = new Health { Maximum = 400, Current = 400 },
                Stamina = new Health { Maximum = 300, Current = 300 },
                Location = new Position { X = 3, Y = 4 },
                Orientation = Orientation.West,
                Script = "if( LastAction == MOVING_FORWARD ){ TurnLeft(); }else{ MoveForward(); }".Base64Encode()
            };
            await new BotClient().CreateBot(bot1);
            var bot2 = new Bot
            {
                Name = "SjarelA2",
                Avatar = Avatar.BeigeDread,
                PhysicalHealth = new Health { Maximum = 400, Current = 400 },
                Stamina = new Health { Maximum = 300, Current = 300 },
                Location = new Position { X = 6, Y = 9 },
                Orientation = Orientation.West,
                Script = "if( LastAction == MOVING_FORWARD ){ TurnRight(); }else{ MoveForward(); }".Base64Encode()
            };
            await new BotClient().CreateBot(bot2);
            var bot3 = new Bot
            {
                Name = "SjarelA3",
                Avatar = Avatar.BeigeDread,
                PhysicalHealth = new Health { Maximum = 400, Current = 400 },
                Stamina = new Health { Maximum = 300, Current = 300 },
                Location = new Position { X = 10, Y = 11 },
                Orientation = Orientation.West,
                Script = "if( (XPosition == 0 && Orientation == WEST) || (XPosition == Width-1 && Orientation == EAST)){ TurnAround(); }else{ MoveForward(); }".Base64Encode()
            };
            await new BotClient().CreateBot(bot3);
        }

        static async Task DeployBots()
        {
            var deployment1 = new Deployment
            {
                ArenaName = "DeathMatchArena",
                BotName = "SjarelA1",
                TeamName = "De Sjarels"
            };
            await new DeploymentClient().Deploy(deployment1);
            var deployment2 = new Deployment
            {
                ArenaName = "DeathMatchArena",
                BotName = "SjarelA2",
                TeamName = "De Sjarels"
            };
            await new DeploymentClient().Deploy(deployment2);
            var deployment3 = new Deployment
            {
                ArenaName = "DeathMatchArena",
                BotName = "SjarelA3",
                TeamName = "De Sjarels"
            };
            await new DeploymentClient().Deploy(deployment3);
        }
    }
}