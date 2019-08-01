using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Common.Processor
{
    public class ValidationResult : IValidationResult
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
