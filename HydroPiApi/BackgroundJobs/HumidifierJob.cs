using HydroPiApi.BackgroundJobs.Models;
using Microsoft.Extensions.Logging;
using RelayClient;
using SensorClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HydroPiApi.BackgroundJobs
{
    public class HumidifierJob : BaseJob
    {
        private readonly ILogger _logger;
        private readonly IHumidifierJobOptions _options;
        

        public HumidifierJob(
            IRelayClient relayClient,
            ISensorClient sensorClient,
            ILoggerFactory loggerFactory) : base(relayClient,sensorClient)
        {
            _options = (HumidifierJobOptions)JobStateHelper.JobStateHelper
                .GetJobByName("HumidifierJob").JobOptions;

            _logger = loggerFactory.CreateLogger<HumidifierJob>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // This will cause the loop to stop if the service is stopped
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //var humidityReading = (HumidityTemperatureReading)_sensorClient
                    //    .GetSensorReading(new SensorReadingByGpioOptions() {GpioPin = _options.HumiditySensorGpio });

                    var relayRequest = new ToggleRelayStateRequest
                    {
                        GpioPin = _options.RelayGpio,
                        State = RelayState.On
                    };

                    if (_relayClient.GetRelayState(_options.RelayGpio) == RelayState.On)
                    {
                        relayRequest.State = RelayState.Off;
                    }

                    _relayClient.ToggleRelayState(relayRequest);
                }
                catch(Exception ex)
                {
                    //need a logger eventually...
                }
                finally
                {
                    await Task.Delay(TimeSpan.FromMinutes(_options.CheckInterval), stoppingToken);
                }
            }
        }
    }
}
