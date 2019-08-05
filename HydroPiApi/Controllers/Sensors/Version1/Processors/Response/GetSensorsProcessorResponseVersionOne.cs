using HydroPiApi.Controllers.Common.Processor;
using SensorClient.Models;
using System.Collections.Generic;
using System.Net;

namespace HydroPiApi.Controllers.Sensors.Version1.Processors.Response
{
    public class GetSensorsProcessorResponseVersionOne : IProcessorResponse
    {
        public List<Sensor> Sensors{ get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}
