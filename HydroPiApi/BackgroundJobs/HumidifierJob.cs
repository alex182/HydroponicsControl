using HydroPiApi.BackgroundJobs.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RelayClient;
using SensorClient;
using SensorClient.Models.SensorReadings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HydroPiApi.BackgroundJobs
{
    public class HumidifierJob : IHostedService, IDisposable
    {
        private readonly ISensorClient _sensorClient;
        private readonly IRelayClient _relayClient;
        private readonly ILogger _logger;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private readonly IHumidifierJobOptions _options;
        private Task _executingTask;
        

        public HumidifierJob(IRelayClient relayClient, 
            ISensorClient sensorClient,
            ILoggerFactory loggerFactory,
            IHumidifierJobOptions options)
        {
            _relayClient = relayClient;
            _sensorClient = sensorClient;
            _logger = loggerFactory.CreateLogger<HumidifierJob>();
            _options = options;
        }

        protected async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // This will cause the loop to stop if the service is stopped
            while (!stoppingToken.IsCancellationRequested)
            {
                var humidityReading = (HumidityTemperatureReading)_sensorClient
                    .GetSensorReading(_options.HumiditySensorGpio);

                var relayRequest = new ToggleRelayStateRequest {
                    GpioPin = _options.RelayGpio,
                    State = RelayState.On
                };

                if (humidityReading.Humidity >= _options.TargetHumidity)
                {
                    relayRequest.State = RelayState.Off;
                }

                _relayClient.ToggleRelayState(relayRequest);
                await Task.Delay(TimeSpan.FromMinutes(_options.CheckInterval), stoppingToken);
            }
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

        public void Dispose()
        {
            _stoppingCts.Cancel();
        }


    }
}
