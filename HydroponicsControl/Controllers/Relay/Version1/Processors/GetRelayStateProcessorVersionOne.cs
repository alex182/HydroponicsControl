using FluentValidation;
using FluentValidation.Results;
using HydroponicsControl.Controllers.Common.Processor;
using HydroponicsControl.Controllers.Relay.Version1.Processors.Request;
using HydroponicsControl.Controllers.Relay.Version1.Processors.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RelayClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroponicsControl.Controllers.Relay.Version1.Processors
{
    public class GetRelayStateProcessorVersionOne : BaseProcessor<GetRelayStateProcessorRequestVersionOne>
    {
        private readonly ILogger _logger;
        private readonly IRelayClient _relayClient;

        public GetRelayStateProcessorVersionOne(
            GetRelayStateProcessorRequestVersionOne record, 
            ILoggerFactory loggerFactory,
            IRelayClient relayClient) : base(record, loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetRelayStateProcessorVersionOne>();
            _relayClient = relayClient;
        }

        public override ValidationResult IsValid(GetRelayStateProcessorRequestVersionOne record)
        {
            throw new NotImplementedException();
        }

        public override IActionResult ProcessRequest(GetRelayStateProcessorRequestVersionOne record)
        {
            throw new NotImplementedException();
        }
    }
}
