using System;

namespace BotRetreat.Framework.Wpf.Services.Interfaces
{
    public interface ITimerService : IDisposable
    {
        ITimerToken Start(TimeSpan interval, Action action);
    }
}