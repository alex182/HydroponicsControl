using HydroponicsControl.Controllers.Common.Processor;
using HydroponicsControl.Controllers.Relay.Version1.Processors.Request;
using HydroponicsControl.Controllers.Relay.Version1.Processors.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RelayClient;
using System;
using System.Collections.Generic;

namespace HydroponicsControl.Controllers.Relay.Version1.Processors
{
    public class GetRelaysProcessorVersionOne : BaseProcessor<GetRelaysProcessorRequestVersionOne>
    {
        private readonly ILogger _logger;
        private readonly IRelayClient _relayClient;

        public GetRelaysProcessorVersionOne(GetRelaysProcessorRequestVersionOne record, 
            ILoggerFactory loggerFactory,
            IRelayClient relayClient) : base(record, loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetRelaysProcessorVersionOne>();
            _relayClient = relayClient;
        }

        public override IValidationResult IsValid(GetRelaysProcessorRequestVersionOne record)
        {
            return null;
        }

        public override IActionResult ProcessRequest(GetRelaysProcessorRequestVersionOne record)
        {
            var result = _relayClient.GetRelays();

            if(result == null)
            {
                return new ObjectResult(new GetRelaysProcessorResponseVersionOne()
                {
                    IsSuccess = false,
                    Errors = new List<string> { "No relays exist." },
                    Relays = null, 
                    StatusCode = System.Net.HttpStatusCode.NotFound
                });
            }

            return new ObjectResult(new GetRelaysProcessorResponseVersionOne()
            {
                IsSuccess = true,
                Relays = result,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }
    }
}
