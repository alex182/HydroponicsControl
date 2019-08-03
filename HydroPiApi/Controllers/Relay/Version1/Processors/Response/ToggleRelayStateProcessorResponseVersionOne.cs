using HydroPiApi.Controllers.Common.Processor;
using RelayClient;
using System.Collections.Generic;
using System.Net;

namespace HydroPiApi.Controllers.Relay.Version1.Processors.Response
{
    public class ToggleRelayStateProcessorResponseVersionOne : IProcessorResponse
    {
        public int GpioPin { get; set; }
        public RelayState State { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}
