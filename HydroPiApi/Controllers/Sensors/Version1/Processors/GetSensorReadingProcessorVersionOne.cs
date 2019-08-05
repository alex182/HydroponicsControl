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
    public class GetSensorReadingProcessorVersionOne : BaseProcessor<GetSensorReadingProcessorRequestVersionOne>
    {
        private readonly ILogger _logger;
        private readonly ISensorClient _sensorClient;

        public GetSensorReadingProcessorVersionOne(
            GetSensorReadingProcessorRequestVersionOne record,
            ILoggerFactory loggerFactory,
            ISensorClient sensorClient) : base(record, loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetSensorReadingProcessorVersionOne>();
            _sensorClient = sensorClient;
        }

        public override IValidationResult IsValid(GetSensorReadingProcessorRequestVersionOne record)
        {
            var existingSensors = _sensorClient.GetSensors();

            if (existingSensors.FirstOrDefault(r => r.GpioPin == record?.GpioPin) != null)
            {
                return null;
            }

            return new ValidationResult
            {
                Message = $"Could not find a sensor with the gpio pin: {record.GpioPin}",
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }

        public override IActionResult ProcessRequest(GetSensorReadingProcessorRequestVersionOne record)
        {
            var result = new GetSensorReadingProcessorResponseVersionOne
            {
                Errors = new List<string> { $"Could not get reading from gpio pin: {record.GpioPin}" },
                IsSuccess = false,
                Reading = null, 
                StatusCode = System.Net.HttpStatusCode.NotFound
            };

            var reading = _sensorClient.GetSensorReading(record.GpioPin);
            
            if(reading != null)
            {
                result.Errors = null;
                result.IsSuccess = true;
                result.StatusCode = System.Net.HttpStatusCode.OK;
                result.Reading = reading;
            }

            return new ObjectResult(result);
        }
    }
}
