using HydroPiApi.BackgroundJobs.JobStateHelper;
using HydroPiApi.BackgroundJobs.Models;
using HydroPiApi.Controllers.Common.Processor;
using HydroPiApi.Controllers.Tasks.Version1.Processors.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RelayClient;
using SensorClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Tasks.Version1.Processors
{
    public class UpdateHumidifierTaskProcessorVersionOne : BaseProcessor<UpdateHumidifierTaskProcessorRequestVersionOne>
    {

        private readonly ILogger _logger;
        private readonly IRelayClient _relayClient;
        private readonly ISensorClient _sensorClient; 

        public UpdateHumidifierTaskProcessorVersionOne(UpdateHumidifierTaskProcessorRequestVersionOne record,
            ILoggerFactory loggerFactory,
            IRelayClient relayClient,
            ISensorClient sensorClient) : base(record, loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UpdateHumidifierTaskProcessorVersionOne>();
            _relayClient = relayClient;
            _sensorClient = sensorClient;
        }

        public override IValidationResult IsValid(UpdateHumidifierTaskProcessorRequestVersionOne record)
        {
            var existingRelays = _relayClient.GetRelays();
            var existingSensors = _sensorClient.GetSensors(); 
            var jobExists = JobStateHelper.GetJobByName(record.JobName) != null;

            if (record?.RelayGpio != null && !existingRelays.Any(r => r.GpioPin == record.RelayGpio))
            {
                return new ValidationResult
                {
                    Message = $"Could not find a relay with the gpio pin: {record.RelayGpio}",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            if (record?.HumiditySensorGpio != null && !existingSensors.Any(r => r.GpioPin == record.HumiditySensorGpio))
            {
                return new ValidationResult
                {
                    Message = $"Could not find a sensor with the gpio pin: {record.HumiditySensorGpio}",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            if (!jobExists)
            {
                return new ValidationResult
                {
                    Message = $"Could not find any jobs with the name: {record.JobName}",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            return null; 
        }

        public override IActionResult ProcessRequest(UpdateHumidifierTaskProcessorRequestVersionOne record)
        {
            var jobToUpdate = JobStateHelper.GetJobByName(record.JobName);
            var currentJobOptions = (HumidifierPressureAltitudeTemperatureJobOptions)jobToUpdate.JobOptions;

            var newJobState = new JobState()
            { JobOptions = new HumidifierPressureAltitudeTemperatureJobOptions
                {
                    CheckInterval = record?.CheckInterval ?? currentJobOptions.CheckInterval,
                    HumiditySensorGpio = record?.HumiditySensorGpio ?? currentJobOptions.HumiditySensorGpio,
                    TargetHumidity = record?.TargetHumidity ?? currentJobOptions.TargetHumidity,
                    RelayGpio = record?.RelayGpio ?? currentJobOptions.RelayGpio
                },
                LastRunTime = jobToUpdate.LastRunTime,
                NextRunTime = jobToUpdate.NextRunTime
            };

            JobStateHelper.AddOrUpdateJobState(newJobState, record.JobName);

            return new OkObjectResult(newJobState); 
        }
    }
}
