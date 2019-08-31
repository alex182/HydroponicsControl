using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HydroPiApi.BackgroundJobs.Models;
using Microsoft.Extensions.Logging;
using RelayClient;
using SensorClient;

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
            _options = options; 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                await Task.Delay(TimeSpan.FromMinutes(_options.CheckInterval), stoppingToken);
            }
        }
    }
}
