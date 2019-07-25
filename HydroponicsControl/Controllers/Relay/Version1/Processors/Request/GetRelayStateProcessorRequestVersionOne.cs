using HydroponicsControl.Controllers.Common.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroponicsControl.Controllers.Relay.Version1.Processors.Request
{
    public class GetRelayStateProcessorRequestVersionOne : IProcessorRequest
    {
        public int RelayID { get; set; }
    }
}
