using System;
using BotRetreat.Domain;
using BotRetreat.Enums;

namespace BotRetreat.Business.Interfaces
{
    public interface IBot
    {
        Orientation Orientation { get; }

        Position Location { get; }

        String Name { get; }
    }
}