using System;
using System.Windows.Threading;
using BotRetreat.Framework.Wpf.Services.Interfaces;

namespace BotRetreat.Framework.Wpf.Services
{
    public class TimerToken : ITimerToken
    {
        private readonly DispatcherTimer _timer;

        public TimerToken(TimeSpan interval, Action action)
        {
            _timer = new DispatcherTimer { Interval = interval };
            _timer.Tick += (sender, e) => { action(); };
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}