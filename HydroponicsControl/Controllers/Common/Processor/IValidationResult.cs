
using System.Net;

namespace HydroponicsControl.Controllers.Common.Processor
{
    public interface IValidationResult
    {
        string Message { get; set; }
        HttpStatusCode StatusCode { get; set; }
    }
}