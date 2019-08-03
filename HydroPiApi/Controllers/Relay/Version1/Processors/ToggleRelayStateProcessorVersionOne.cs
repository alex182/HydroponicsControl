using HydroPiApi.Controllers.Common.Processor;
using HydroPiApi.Controllers.Relay.Version1.Processors.Request;
using HydroPiApi.Controllers.Relay.Version1.Processors.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RelayClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Relay.Version1.Processors
{
    public class ToggleRelayStateProcessorVersionOne : BaseProcessor<ToggleRelayStateProcessorRequestVersionOne>
    {
        private readonly ILogger _logger;
        private readonly IRelayClient _relayClient;

        public ToggleRelayStateProcessorVersionOne(
            ToggleRelayStateProcessorRequestVersionOne record, 
            ILoggerFactory loggerFactory,
            IRelayClient relayClient) : base(record, loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ToggleRelayStateProcessorVersionOne>();
            _relayClient = relayClient;
        }

        public override IValidationResult IsValid(ToggleRelayStateProcessorRequestVersionOne record)
        {
            var existingRelays = _relayClient.GetRelays();

            if (existingRelays.FirstOrDefault(r => r.GpioPin == record?.GpioPin) != null)
            {
                return null;
            }

            return new ValidationResult
            {
                Message = $"Could not find a relay with the gpio pin: {record.GpioPin}",
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }

        public override IActionResult ProcessRequest(ToggleRelayStateProcessorRequestVersionOne record)
        {
            var result = _relayClient.ToggleRelayState(new ToggleRelayStateRequest {
                GpioPin = record.GpioPin,
                State = record.RelayState
            });

            return new ObjectResult(new ToggleRelayStateProcessorResponseVersionOne {
                State = result.State.Value,
                GpioPin = result.GpioPin,
                IsSuccess = result.IsSuccess,
                StatusCode = System.Net.HttpStatusCode.OK,
                Errors = new List<string> { result?.ErrorMessage }
            });
        }
    }
}
