using HydroPiApi.Controllers.Common.Processor;
using RelayClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Relay.Version1.Processors.Request
{
    public class ToggleRelayStateProcessorRequestVersionOne : IProcessorRequest
    {
        public int GpioPin { get; set; }
        public RelayState RelayState { get; set; }
    }
}
