using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using BotRetreat.Framework.Wpf.Services.Interfaces;

namespace BotRetreat.Framework.Wpf.Services.Design
{
    public class DesignTimerService : ITimerService
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private Action _action;

        public ITimerToken Start(TimeSpan interval, Action action)
        {
            _action = action;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
            return new DesignTimerToken();
        }

        private void Timer_Tick(Object sender, EventArgs e)
        {
            _timer.Stop();
            _action();
        }

        public void Dispose()
        {
            _timer.Stop();
        }
    }
}