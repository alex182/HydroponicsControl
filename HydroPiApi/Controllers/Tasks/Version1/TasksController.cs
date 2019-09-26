using HydroPiApi.BackgroundJobs.JobStateHelper;
using HydroPiApi.Controllers.Common.Processor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Tasks.Version1
{
    [Route("api/task")]
    [ApiController]
    [ApiVersion("1")]

    public class TasksController: Controller
    {
        private readonly ILogger _logger;
        private readonly IProcessorFactory _processorFactory;

        public TasksController(
            ILoggerFactory loggerFactory,
            IProcessorFactory processorFactory)
        {
            _logger = loggerFactory.CreateLogger<TasksController>();
            _processorFactory = processorFactory;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return new OkObjectResult(JobStateHelper.GetJobs());
        }

        [HttpGet("")]
    }
}
