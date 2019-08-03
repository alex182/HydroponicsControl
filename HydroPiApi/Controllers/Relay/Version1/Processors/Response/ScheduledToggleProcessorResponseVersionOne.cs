using System.Collections.Generic;
using System.Net;
using HydroPiApi.Controllers.Common.Processor;

namespace HydroPiApi.Controllers.Relay.Version1.Processors.Response
{
    public class ScheduledToggleProcessorResponseVersionOne : IProcessorResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}
