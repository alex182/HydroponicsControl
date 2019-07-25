using HydroponicsControl.Controllers.Common.Processor;
using HydroponicsControl.Controllers.Relay.Version1.Models;
using HydroponicsControl.Controllers.Relay.Version1.Processors.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RelayClient;
using System;
using System.ComponentModel.DataAnnotations;
using ToggleRelayStateRequest = HydroponicsControl.Controllers.Relay.Version1.Models.ToggleRelayStateRequest;

namespace HydroponicsControl.Controllers.Relay.Version1
{
    [Route("api/relay")]
    [ApiController]
    [ApiVersion("1")]
    public class RelayController : Controller
    {
        private readonly ILogger _logger;
        private readonly IRelayClient _relayClient;
        private readonly IProcessorFactory _processorFactory;

        public RelayController(ILoggerFactory loggerFactory, 
            IRelayClient relayClient,
            IProcessorFactory processorFactory)
        {
            _logger = loggerFactory.CreateLogger<RelayController>();
            _relayClient = relayClient;
            _processorFactory = processorFactory; 
        }


        [HttpGet("state")]
        public IActionResult GetRelayState([FromQuery][Range(1,int.MaxValue)]int relayId)
        {
            var processorRequest = new GetRelayStateProcessorRequestVersionOne();
            var processor = _processorFactory.Create(processorRequest);

            var result = processor.Execute();

            return new ObjectResult(result);
        }

        [HttpGet()]
        public IActionResult GetRelays()
        {
            throw new NotImplementedException();
        }

        [HttpPost("toggleState")]
        public IActionResult ToggleRelayState([FromBody] ToggleRelayStateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
