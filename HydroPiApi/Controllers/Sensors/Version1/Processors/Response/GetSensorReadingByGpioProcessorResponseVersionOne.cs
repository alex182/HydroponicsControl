using HydroPiApi.Controllers.Common.Processor;
using SensorClient.Models.SensorReadings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Sensors.Version1.Processors.Response
{
    public class GetSensorReadingByGpioProcessorResponseVersionOne : IProcessorResponse
    {
        public ISensorReading Reading { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}
