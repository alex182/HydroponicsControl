using HydroPiApi.Controllers.Common.Processor;
using HydroPiApi.Controllers.Relay.Version1.Processors.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RelayClient;
using System;
using System.Linq;

namespace HydroPiApi.Controllers.Relay.Version1.Processors
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

        public override IValidationResult IsValid(GetRelayStateProcessorRequestVersionOne record)
        {
            var existingRelays = _relayClient.GetRelays();

            if(record?.Pin == null && record?.GpioPin == null && string.IsNullOrWhiteSpace(record?.RelayName))
            {
                return new ValidationResult
                {
                    Message = $"At least one of the following must be included " +
                    $"{nameof(record.Pin)}, {nameof(record.GpioPin)}, or {nameof(record.RelayName)}",

                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            if (existingRelays.FirstOrDefault(r => r.Pin == record?.Pin) != null)
            {
                return null; 
            }

            if (existingRelays.FirstOrDefault(r => r.GpioPin == record?.GpioPin) != null)
            {
                return null;
            }

            if (existingRelays.FirstOrDefault(r => r.RelayName.ToLower() == record.RelayName?.ToLower()) != null)
            {
                return null;
            }

            return new ValidationResult
            {
                Message = "Could not find a relay using the provided information",
                StatusCode = System.Net.HttpStatusCode.NotFound
            }; 
        }

        public override IActionResult ProcessRequest(GetRelayStateProcessorRequestVersionOne record)
        {
            throw new NotImplementedException();
        }
    }
}
