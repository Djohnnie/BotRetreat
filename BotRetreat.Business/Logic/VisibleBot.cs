using System;
using BotRetreat.Business.Interfaces;
using BotRetreat.Domain;
using BotRetreat.Enums;

namespace BotRetreat.Business.Logic
{
    public class VisibleBot : IBot
    {
        public Orientation Orientation { get; }

        public Position Location { get; }

        public String Name { get; }

        public VisibleBot(Bot bot)
        {
            Orientation = bot.Orientation;
            Location = bot.Location;
            Name = bot.Name;
        }

    }
}