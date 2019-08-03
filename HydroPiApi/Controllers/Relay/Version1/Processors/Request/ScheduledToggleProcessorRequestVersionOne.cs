using HydroPiApi.Controllers.Common.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Relay.Version1.Processors.Request
{
    public class ScheduledToggleProcessorRequestVersionOne: IProcessorRequest
    {
        public int GpioPin { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Stop { get; set; }
        public TimeSpan IntervalOn { get; set; }
        public TimeSpan IntervalOff { get; set; }
    }
}
