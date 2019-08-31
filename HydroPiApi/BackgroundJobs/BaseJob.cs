using HydroPiApi.BackgroundJobs.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RelayClient;
using SensorClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HydroPiApi.BackgroundJobs
{
    public abstract class BaseJob : IHostedService, IDisposable
    {
        protected readonly ISensorClient _sensorClient;
        protected readonly IRelayClient _relayClient;
        protected readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        protected Task _executingTask;


        public BaseJob(IRelayClient relayClient,
            ISensorClient sensorClient)
        {
            _relayClient = relayClient;
            _sensorClient = sensorClient;
        }

        public void Dispose()
        {
            _stoppingCts.Cancel();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            return Task.CompletedTask;
        }

        protected virtual async Task ExecuteAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(0), token);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                    cancellationToken));
            }
        }
    }
}
