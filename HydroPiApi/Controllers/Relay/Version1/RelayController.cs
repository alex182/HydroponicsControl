using HydroPiApi.Controllers.Common.Processor;
using HydroPiApi.Controllers.Relay.Version1.Models;
using HydroPiApi.Controllers.Relay.Version1.Processors.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RelayClient;
using System;
using System.ComponentModel.DataAnnotations;
using ToggleRelayStateRequest = HydroPiApi.Controllers.Relay.Version1.Models.ToggleRelayStateRequest;

namespace HydroPiApi.Controllers.Relay.Version1
{
    [Route("api/relay")]
    [ApiController]
    [ApiVersion("1")]
    public class RelayController : Controller
    {
        private readonly ILogger _logger;
        private readonly IProcessorFactory _processorFactory;

        public RelayController(ILoggerFactory loggerFactory, 
            IProcessorFactory processorFactory)
        {
            _logger = loggerFactory.CreateLogger<RelayController>();
            _processorFactory = processorFactory; 
        }


        [HttpGet("state")]
        public IActionResult GetRelayState([FromQuery]GetRelayStateRequest request)
        {
            var processorRequest = new GetRelayStateProcessorRequestVersionOne() {
                GpioPin = request.GpioPin
            };
            var processor = _processorFactory.Create(processorRequest);

            var result = processor.Execute();

            return result;
        }

        [HttpGet()]
        public IActionResult GetRelays()
        {
            var processor = _processorFactory.Create(new GetRelaysProcessorRequestVersionOne());
            return processor.Execute(); 
        }

        [HttpPost("toggleState")]
        public IActionResult ToggleRelayState([FromBody] ToggleRelayStateRequest request)
        {
            var processorRequest = new ToggleRelayStateProcessorRequestVersionOne
            {
                GpioPin = request.GpioPin,
                RelayState = request.RelayState
            };

            var processor = _processorFactory.Create(processorRequest);

            var result = processor.Execute();

            return result;
        }

        [HttpPost("scheduleToggle")]
        public IActionResult ScheduleToggle([FromBody] ScheduledToggleRequest request)
        {
            var processorRequest = new ScheduledToggleProcessorRequestVersionOne
            {
                GpioPin = request.GpioPin,
                Start = request.Start,
                Stop = request?.Stop,
                IntervalOn = request.IntervalOn,
                IntervalOff = request.IntervalOff
            };

            var processor = _processorFactory.Create(processorRequest);

            var result = processor.Execute();

            return result;
        }
    }
}
