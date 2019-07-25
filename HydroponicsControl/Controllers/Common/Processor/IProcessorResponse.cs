using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HydroponicsControl.Controllers.Common.Processor
{
    public interface IProcessorResponse
    {
        HttpStatusCode StatusCode { get; set; }
        bool IsSuccess { get; set; }
        List<string> Errors { get; set; }
    }
}
