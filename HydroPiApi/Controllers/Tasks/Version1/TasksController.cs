using HydroPiApi.BackgroundJobs.JobStateHelper;
using HydroPiApi.Controllers.Common.Processor;
using HydroPiApi.Controllers.Tasks.Version1.Models;
using HydroPiApi.Controllers.Tasks.Version1.Processors.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RelayClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [HttpGet()]
        public IActionResult GetAllTasks()
        {
            return new OkObjectResult(JobStateHelper.GetJobs());
        }

        [HttpPut("updateHumidifierTask")]
        public IActionResult UpdateHumidifierTask(UpdateHumidifierTaskRequest request)
        {
            var processorRequest = new UpdateHumidifierTaskProcessorRequestVersionOne
            {
                CheckInterval = request?.CheckInterval,
                HumiditySensorGpio = request?.HumiditySensorGpio,
                TargetHumidity = request?.TargetHumidity,
                RelayGpio = request?.RelayGpio,
                JobName = request.JobName
            };

            var processor = _processorFactory.Create(processorRequest);
            var result = processor.Execute();

            return result; 
        }

        [HttpGet("ByName")]
        public IActionResult GetTaskByName([FromQuery][Required]string taskName)
        {
            var request = new GetTaskByNameProcessorRequestVersionOne()
            {
                TaskName = taskName
            };

            var processor = _processorFactory.Create(request);
            return processor.Execute(); 
        }
    }
}
