using HydroponicsControl.Controllers.Relay.Version1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RelayClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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

        public RelayController(ILogger logger, 
            IRelayClient relayClient)
        {
            _logger = logger;
            _relayClient = relayClient;
        }


        [HttpGet("state")]
        public IActionResult GetRelayState([FromQuery][Range(1,int.MaxValue)]int relayId)
        {
            throw new NotImplementedException();
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
