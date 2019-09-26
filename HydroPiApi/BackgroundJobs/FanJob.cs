using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HydroPiApi.BackgroundJobs.JobStateHelper;
using HydroPiApi.BackgroundJobs.Models;
using Microsoft.Extensions.Logging;
using RelayClient;
using SensorClient;

namespace HydroPiApi.BackgroundJobs
{
    public class FanJob : BaseJob
    {
        private readonly ILogger _logger;
        private FanJobOptions _options;

        public FanJob(IRelayClient relayClient, 
            ISensorClient sensorClient,
            ILoggerFactory loggerFactory,
            FanJobOptions options) : base(relayClient, sensorClient)
        {
            _logger = loggerFactory.CreateLogger<FanJob>();
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                try
                {
                    var relayRequest = new ToggleRelayStateRequest
                    {
                        GpioPin = _options.RelayGpioPin,
                        State = RelayState.On
                    };

                    _relayClient.ToggleRelayState(relayRequest);

                    await Task.Delay(TimeSpan.FromMinutes(_options.RunDuration), stoppingToken);

                    relayRequest.State = RelayState.Off;

                    _relayClient.ToggleRelayState(relayRequest);
                }
                catch (Exception ex)
                {
                    // need a logger eventually...
                }
                finally
                {
                    var lastRun = DateTime.UtcNow;
                    JobStateHelper.JobStateHelper.AddOrUpdateJobState(new JobState
                    {
                        LastRunTime = lastRun,
                        NextRunTime = lastRun.AddMinutes(_options.JobInterval)
                    }, nameof(FanJob));

                    await Task.Delay(TimeSpan.FromMinutes(_options.JobInterval), stoppingToken);
                }
            }
        }
    }
}
