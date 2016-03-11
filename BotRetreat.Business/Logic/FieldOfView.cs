using System.Collections.Generic;
using System.Linq;
using BotRetreat.Business.Interfaces;
using BotRetreat.Domain;
using BotRetreat.Enums;

namespace BotRetreat.Business.Logic
{
    public class FieldOfView : IFieldOfView
    {
        public List<IBot> Bots { get; }
        public List<IBot> FriendlyBots { get; }
        public List<IBot> EnemyBots { get; }
        public List<IBot> PredatorBots { get; }

        public FieldOfView(Arena arena, Bot bot, List<Bot> bots)
        {
            Bots = new List<IBot>();
            FriendlyBots = new List<IBot>();
            EnemyBots = new List<IBot>();
            PredatorBots = new List<IBot>();
            var currentTeamName = bot.Deployments.Select(x => x.Team.Name).Distinct().Single();
            var minimumX = 0;
            var minimumY = 0;
            var maximumX = arena.Width - 1;
            var maximumY = arena.Height - 1;
            switch (bot.Orientation)
            {
                case Orientation.North:
                    minimumX = 0;
                    maximumX = arena.Width - 1;
                    minimumY = 0;
                    maximumY = bot.Location.Y;
                    break;
                case Orientation.East:
                    minimumX = bot.Location.X;
                    maximumX = arena.Width - 1;
                    minimumY = 0;
                    maximumY = arena.Height - 1;
                    break;
                case Orientation.South:
                    minimumX = 0;
                    maximumX = arena.Width - 1;
                    minimumY = bot.Location.Y;
                    maximumY = arena.Height - 1;
                    break;
                case Orientation.West:
                    minimumX = 0;
                    maximumX = bot.Location.X;
                    minimumY = 0;
                    maximumY = arena.Height - 1;
                    break;
            }
            foreach (var otherBot in bots)
            {
                if (otherBot.Id != bot.Id)
                {
                    if (otherBot.Location.X >= minimumX && otherBot.Location.X <= maximumX &&
                        otherBot.Location.Y >= minimumY && otherBot.Location.Y <= maximumY)
                    {
                        Bots.Add(new VisibleBot(otherBot));
                        var botTeamName = otherBot.Deployments.Select(x => x.Team.Name).Distinct().Single();
                        if (botTeamName == currentTeamName)
                        {
                            FriendlyBots.Add(new VisibleBot(otherBot));
                        }
                        else
                        {
                            EnemyBots.Add(new VisibleBot(otherBot));
                        }
                    }
                }
            }
        }
    }
}