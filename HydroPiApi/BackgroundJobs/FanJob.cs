using System;
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
            ILoggerFactory loggerFactory) : base(relayClient, sensorClient)
        {
            _logger = loggerFactory.CreateLogger<FanJob>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _options = (FanJobOptions)JobStateHelper.JobStateHelper.GetJobByName("FanJob").JobOptions;

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
                        NextRunTime = lastRun.AddMinutes(_options.JobInterval),
                        JobOptions = _options
                    }, nameof(FanJob));

                    await Task.Delay(TimeSpan.FromMinutes(_options.JobInterval), stoppingToken);
                }
            }
        }
    }
}
