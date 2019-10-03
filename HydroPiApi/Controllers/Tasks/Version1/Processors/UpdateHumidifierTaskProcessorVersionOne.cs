using HydroPiApi.BackgroundJobs.JobStateHelper;
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
    public class UpdateHumidifierTaskProcessorVersionOne : BaseProcessor<UpdateHumidifierTaskProcessorRequestVersionOne>
    {

        private readonly ILogger _logger;
        private readonly IRelayClient _relayClient;

        public UpdateHumidifierTaskProcessorVersionOne(UpdateHumidifierTaskProcessorRequestVersionOne record,
            ILoggerFactory loggerFactory,
            IRelayClient relayClient) : base(record, loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UpdateHumidifierTaskProcessorVersionOne>();
            _relayClient = relayClient;
        }

        public override IValidationResult IsValid(UpdateHumidifierTaskProcessorRequestVersionOne record)
        {
            var existingRelays = _relayClient.GetRelays();
            var jobExists = JobStateHelper.GetJobByName(record.JobName) != null;

            if (!existingRelays.Any(r => r.GpioPin == record.HumiditySensorGpio))
            {
                return new ValidationResult
                {
                    Message = $"Could not find a sensor with the gpio pin: {record.HumiditySensorGpio}",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            if(jobExists)
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
            //var jobToUpdate = JobStateHelper.GetJobByName(record.JobName);
            //var newJobState = new JobState() { }

            //JobStateHelper.AddOrUpdateJobState()

            throw new NotImplementedException(); 
        }
    }
}
