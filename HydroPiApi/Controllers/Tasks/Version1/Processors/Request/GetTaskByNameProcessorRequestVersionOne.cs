using HydroPiApi.Controllers.Common.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Tasks.Version1.Processors.Request
{
    public class GetTaskByNameProcessorRequestVersionOne : IProcessorRequest
    {
        public string TaskName { get; set; }
    }
}
