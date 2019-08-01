
using System.Net;

namespace HydroPiApi.Controllers.Common.Processor
{
    public interface IValidationResult
    {
        string Message { get; set; }
        HttpStatusCode StatusCode { get; set; }
    }
}