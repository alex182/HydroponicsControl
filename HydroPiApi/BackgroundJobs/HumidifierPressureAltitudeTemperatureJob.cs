using System;
using System.Threading;
using System.Threading.Tasks;
using HydroPiApi.BackgroundJobs.JobStateHelper;
using HydroPiApi.BackgroundJobs.Models;
using Microsoft.Extensions.Logging;
using RelayClient;
using SensorClient;
using SensorClient.Models.SensorReadings;
using SensorClient.SensorReadings.Clients.Models;

namespace HydroPiApi.BackgroundJobs
{
    public class HumidifierPressureAltitudeTemperatureJob : BaseJob
    {
        private readonly ILogger _logger;
        private HumidifierPressureAltitudeTemperatureJobOptions _options; 

        public HumidifierPressureAltitudeTemperatureJob(
            IRelayClient relayClient, 
            ISensorClient sensorClient,
            ILoggerFactory loggerFactory,
            HumidifierPressureAltitudeTemperatureJobOptions options) : base(relayClient, sensorClient)
        {
            _logger = loggerFactory.CreateLogger<HumidifierPressureAltitudeTemperatureJob>();

            _options = (HumidifierPressureAltitudeTemperatureJobOptions)
                JobStateHelper.JobStateHelper
                .GetJobByName("HumidifierPressureAltitudeTemperatureJob").JobOptions; 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                try
                {
                    var humidityReading = (HumidityTemperatureAltitudePressureReading)_sensorClient
                        .GetSensorReading(new SensorReadingByGpioI2COptions() {GpioPin = _options.HumiditySensorGpio });

                    var relayRequest = new ToggleRelayStateRequest
                    {
                        GpioPin = _options.RelayGpio,
                        State = RelayState.Off
                    };

                    if (humidityReading.Humidity < _options.TargetHumidity)
                    {
                        relayRequest.State = RelayState.On;
                    }

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
                        NextRunTime = lastRun.AddMinutes(_options.CheckInterval),
                        JobOptions = _options
                    }, nameof(HumidifierPressureAltitudeTemperatureJob));

                    await Task.Delay(TimeSpan.FromMinutes(_options.CheckInterval), stoppingToken);
                }
            }
        }
    }
}
