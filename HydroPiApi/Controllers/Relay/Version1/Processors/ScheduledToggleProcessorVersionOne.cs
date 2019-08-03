using HydroPiApi.Controllers.Common.Processor;
using HydroPiApi.Controllers.Relay.Version1.Processors.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Relay.Version1.Processors
{
    public class ScheduledToggleProcessorVersionOne : BaseProcessor<ScheduledToggleProcessorRequestVersionOne>
    {
        //TODO: Decide on how to run background jobs and create a client for that then inject it here. 
        private readonly ILogger _logger; 

        public ScheduledToggleProcessorVersionOne(ScheduledToggleProcessorRequestVersionOne record,
            ILoggerFactory loggerFactory) : base(record, loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ScheduledToggleProcessorVersionOne>(); 
        }

        public override IValidationResult IsValid(ScheduledToggleProcessorRequestVersionOne record)
        {
            throw new NotImplementedException();
        }

        public override IActionResult ProcessRequest(ScheduledToggleProcessorRequestVersionOne record)
        {
            throw new NotImplementedException();
        }
    }
}
