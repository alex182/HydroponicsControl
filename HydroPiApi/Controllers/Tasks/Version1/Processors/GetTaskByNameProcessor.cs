using HydroPiApi.BackgroundJobs.JobStateHelper;
using HydroPiApi.Controllers.Common.Processor;
using HydroPiApi.Controllers.Tasks.Version1.Processors.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Tasks.Version1.Processors
{
    public class GetTaskByNameProcessor : BaseProcessor<GetTaskByNameProcessorRequestVersionOne>
    {
        public GetTaskByNameProcessor(
            GetTaskByNameProcessorRequestVersionOne record, 
            ILoggerFactory loggerFactory) : base(record, loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetTaskByNameProcessor>();
        }

        public override IValidationResult IsValid(GetTaskByNameProcessorRequestVersionOne record)
        {
            return null;
        }

        public override IActionResult ProcessRequest(GetTaskByNameProcessorRequestVersionOne record)
        {
            var task = JobStateHelper.GetJobByName(record.TaskName);

            if(task == null) 
                return new NotFoundObjectResult(record);

            return new OkObjectResult(task);
        }
    }
}
