using HydroPiApi.Controllers.Common.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Tasks.Version1.Processors.Request
{
    public class UpdateFanTaskProcessorRequestVersionOne : IProcessorRequest
    {
        public int? RelayGpioPin { get; set; }
        public int? RunDuration { get; set; }
        public int? RunInterval { get; set; }
        public string JobName { get; set; }
    }
}
