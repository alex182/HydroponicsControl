
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Common.Processor
{
    public interface IProcessor
    {
        IActionResult Execute();
    }
}