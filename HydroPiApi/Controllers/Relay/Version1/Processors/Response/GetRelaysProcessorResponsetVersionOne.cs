using HydroPiApi.Controllers.Common.Processor;
using System.Collections.Generic;
using System.Net;

namespace HydroPiApi.Controllers.Relay.Version1.Processors.Response
{
    public class GetRelaysProcessorResponseVersionOne : IProcessorResponse
    {
        public List<RelayClient.Models.Relay> Relays { get; set; } = new List<RelayClient.Models.Relay>();
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
