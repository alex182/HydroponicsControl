using HydroPiApi.BackgroundJobs.JobStateHelper;
using HydroPiApi.BackgroundJobs.Models;
using HydroPiApi.Controllers.Common.Processor;
using HydroPiApi.Controllers.Tasks.Version1.Processors.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RelayClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Tasks.Version1.Processors
{
    public class UpdateFanTaskProcessorVersionOne : BaseProcessor<UpdateFanTaskProcessorRequestVersionOne>
    {
        private readonly ILogger _logger;
        private readonly IRelayClient _relayClient;

        public UpdateFanTaskProcessorVersionOne(UpdateFanTaskProcessorRequestVersionOne record,
            ILoggerFactory loggerFactory, 
            IRelayClient relayClient) : base(record, loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UpdateFanTaskProcessorVersionOne>();
            _relayClient = relayClient;
        }

        public override IValidationResult IsValid(UpdateFanTaskProcessorRequestVersionOne record)
        {
            var existingRelays = _relayClient.GetRelays();
            var jobExists = JobStateHelper.GetJobByName(record.JobName) != null;

            if (record?.RelayGpioPin != null && !existingRelays.Any(r => r.GpioPin == record.RelayGpioPin))
            {
                return new ValidationResult
                {
                    Message = $"Could not find a relay with the gpio pin: {record.RelayGpioPin}",
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

        public override IActionResult ProcessRequest(UpdateFanTaskProcessorRequestVersionOne record)
        {
            var jobToUpdate = JobStateHelper.GetJobByName(record.JobName);
            var currentJobOptions = (FanJobOptions)jobToUpdate.JobOptions;

            var newJobState = new JobState()
            {
                JobOptions = new FanJobOptions
                {
                    RunInterval = record?.RunInterval ?? currentJobOptions.RunInterval,
                    RunDuration = record?.RunDuration ?? currentJobOptions.RunDuration,
                    RelayGpioPin = record?.RelayGpioPin ?? currentJobOptions.RelayGpioPin
                },
                LastRunTime = jobToUpdate.LastRunTime,
                NextRunTime = jobToUpdate.NextRunTime
            };

            JobStateHelper.AddOrUpdateJobState(newJobState, record.JobName);

            return new OkObjectResult(newJobState);
        }
    }
}
