using HydroPiApi.Controllers.Common.Processor;
using RelayClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Relay.Version1.Processors.Response
{
    public class ToggleRelayStateProcessorResponseVersionOne : IProcessorResponse
    {
        public RelayState State { get; set; }
        public int GpioPin { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}
