
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HydroponicsControl.Controllers.Common.Processor
{
    public interface IProcessor
    {
        IActionResult Execute();
    }
}