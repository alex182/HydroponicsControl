using HydroPiApi.Controllers.Common.Processor;
using HydroPiApi.Controllers.Relay.Version1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Relay.Version1.Processors.Response
{
    public class GetRelayStateProcessorResponseVersionOne : IProcessorResponse
    {
        public RelayState State { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}
