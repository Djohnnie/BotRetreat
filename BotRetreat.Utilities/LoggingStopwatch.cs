using System;
using System.Diagnostics;

namespace BotRetreat.Utilities
{
    public class LoggingStopwatch : IDisposable
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly String _logMessage;

        public LoggingStopwatch(String logMessage)
        {
            _stopwatch.Start();
            _logMessage = logMessage;
        }
        public void Dispose()
        {
            Debug.WriteLine($"{_logMessage} took {_stopwatch.ElapsedMilliseconds} ms to complete.");
            _stopwatch.Stop();
        }
    }
}