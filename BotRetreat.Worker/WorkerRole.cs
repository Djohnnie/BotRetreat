using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BotRetreat.Business.Interfaces;
using BotRetreat.Business.Unity;
using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace BotRetreat.Worker
{
    public class WorkerRole : RoleEntryPoint
    {
        private const Int32 DELAY_MS = 2000;

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent _runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("BotRetreat.Worker is running");

            try
            {
                var container = new UnityContainer();
                container.AddNewExtension<BusinessContainerExtension>();
                this.RunAsync(this._cancellationTokenSource.Token, container).Wait();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
            }
            finally
            {
                this._runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            var result = base.OnStart();

            Trace.TraceInformation("BotRetreat.Worker has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("BotRetreat.Worker is stopping");

            this._cancellationTokenSource.Cancel();
            this._runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("BotRetreat.Worker has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken, UnityContainer container)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var start = DateTime.UtcNow;
                    var sw = Stopwatch.StartNew();
                    var core = container.Resolve<ICoreLogic>();
                    await core.Go(cancellationToken);
                    sw.Stop();
                    Debug.WriteLine($"CORE DID {sw.ElapsedMilliseconds} ms");
                    var timeTaken = DateTime.UtcNow - start;
                    var delay = (Int32)(timeTaken.TotalMilliseconds < DELAY_MS ? DELAY_MS - timeTaken.TotalMilliseconds : 0);
                    await Task.Delay(delay, cancellationToken);
                }
                catch (Exception ex)
                {
                    Trace.TraceInformation(ex.ToString());
                }
            }
        }
    }
}