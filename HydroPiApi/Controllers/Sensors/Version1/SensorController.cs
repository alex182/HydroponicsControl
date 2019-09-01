using HydroPiApi.Controllers.Common.Processor;
using HydroPiApi.Controllers.Sensors.Version1.Processors.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [HttpGet("reading")]
        public IActionResult GetSensorReadingByGpio([FromQuery][Required][Range(0,int.MaxValue)] int gpioPin)
        {
            var request = new GetSensorReadingByGpioProcessorRequestVersionOne()
            {
                GpioPin = gpioPin
            };
            var processor = _processorFactory.Create(request);

            var result = processor.Execute();
            return result; 
        }

        [HttpGet("reading")]
        public IActionResult GetSensorReadingByGpioAndI2C([FromQuery][Required][Range(0,int.MaxValue)] int gpiopin,
            [FromQuery][Required][Range(0,int.MaxValue)] int i2cpin)
        {
            var request = new GetSensorReadingByGpioAndI2CProcessorRequestVersionOne()
            {
                GpioPin = gpiopin,
                I2CPin = i2cpin
            };

            var processor = _processorFactory.Create(request);

            var result = processor.Execute();
            return result;
        }
    }
}
