using HydroPiApi.Controllers.Common.Processor;
using HydroPiApi.Controllers.Sensors.Version1.Processors.Request;
using HydroPiApi.Controllers.Sensors.Version1.Processors.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SensorClient;
using SensorClient.SensorReadings.Clients.Models;
using System.Collections.Generic;
using System.Linq;

namespace HydroPiApi.Controllers.Sensors.Version1.Processors
{
    public class GetSensorReadingByGpioAndI2CProcessorVersionOne : BaseProcessor<GetSensorReadingByGpioAndI2CProcessorRequestVersionOne>
    {
        private readonly ILogger _logger;
        private readonly ISensorClient _sensorClient;

        public GetSensorReadingByGpioAndI2CProcessorVersionOne(
            GetSensorReadingByGpioAndI2CProcessorRequestVersionOne record, 
            ILoggerFactory loggerFactory,
            ISensorClient sensorClient) : base(record, loggerFactory)
        {
            _sensorClient = sensorClient;
            _logger = loggerFactory.CreateLogger<GetSensorReadingByGpioAndI2CProcessorVersionOne>();
        }

        public override IValidationResult IsValid(GetSensorReadingByGpioAndI2CProcessorRequestVersionOne record)
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

        public override IActionResult ProcessRequest(GetSensorReadingByGpioAndI2CProcessorRequestVersionOne record)
        {
            var result = new GetSensorReadingByGpioAndI2CProcessorResponseVersionOne
            {
                Errors = new List<string> { $"Could not get reading from gpio pin: {record.GpioPin}" },
                IsSuccess = false,
                Reading = null,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };

            var reading = _sensorClient.GetSensorReading(new SensorReadingByGpioI2COptions()
            {
                GpioPin = record.GpioPin,
                I2CPin = record.I2CPin
            });

            if (reading != null)
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
