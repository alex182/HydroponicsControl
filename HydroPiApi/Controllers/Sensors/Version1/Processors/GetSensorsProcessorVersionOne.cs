using HydroPiApi.Controllers.Common.Processor;
using HydroPiApi.Controllers.Sensors.Version1.Processors.Request;
using HydroPiApi.Controllers.Sensors.Version1.Processors.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SensorClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Sensors.Version1.Processors
{
    public class GetSensorsProcessorVersionOne : BaseProcessor<GetSensorsProcessorRequestVersionOne>
    {
        private readonly ILogger _logger;
        private readonly ISensorClient _sensorClient; 

        public GetSensorsProcessorVersionOne(
            GetSensorsProcessorRequestVersionOne record, 
            ILoggerFactory loggerFactory,
            ISensorClient sensorClient) : base(record, loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetSensorsProcessorVersionOne>();
            _sensorClient = sensorClient; 
        }

        public override IValidationResult IsValid(GetSensorsProcessorRequestVersionOne record)
        {
            return null;
        }

        public override IActionResult ProcessRequest(GetSensorsProcessorRequestVersionOne record)
        {
            var result = new GetSensorsProcessorResponseVersionOne
            {
                Errors = new List<string> { "No sensors exist" },
                IsSuccess = false,
                Sensors = null,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };

            var sensors = _sensorClient.GetSensors();

            if (sensors.Any())
            {
                result.Errors = null;
                result.Sensors = sensors;
                result.StatusCode = System.Net.HttpStatusCode.OK;
                result.IsSuccess = true;
            }

            return new ObjectResult(result);
        }
    }
}
