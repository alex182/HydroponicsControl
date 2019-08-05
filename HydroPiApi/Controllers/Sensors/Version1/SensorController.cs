using HydroPiApi.Controllers.Common.Processor;
using HydroPiApi.Controllers.Sensors.Version1.Processors.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Sensors.Version1
{
    [Route("api/sensor")]
    [ApiController]
    [ApiVersion("1")]

    public class SensorController : Controller
    {
        private readonly ILogger _logger;
        private readonly IProcessorFactory _processorFactory;


        public SensorController(ILoggerFactory loggerFactory,
            IProcessorFactory processorFactory)
        {
            _logger = loggerFactory.CreateLogger<SensorController>();
            _processorFactory = processorFactory;
        }

        [HttpGet()]
        public IActionResult GetSensors()
        {
            var request = new GetSensorsProcessorRequestVersionOne();
            var processor = _processorFactory.Create(request);

            var result = processor.Execute();

            return result; 
        }
    }
}
