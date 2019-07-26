using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HydroponicsControl.Controllers.Common.Processor
{
    public abstract class BaseProcessor<T> : IProcessor where T : IProcessorRequest
    {
        private readonly T _record;
        protected ILogger<BaseProcessor<T>> _logger;

        protected BaseProcessor(T record, ILoggerFactory loggerFactory)
        {
            _record = record;
            _logger = loggerFactory.CreateLogger<BaseProcessor<T>>();
        }

        public IActionResult Execute()
        {
            try
            {
                var validationResult = IsValid(_record);

                if (validationResult == null)
                {
                    return ProcessRequest(_record);
                }

                return new ObjectResult(validationResult);
            }
            catch (Exception e)
            {
                var errorId = Guid.NewGuid();

                var message = $"Unable to process request. See ErrorId for details. ErrorId: {errorId}";

                _logger.LogError(e, message);

                return new ContentResult
                {
                    ContentType = "application/json",
                    Content = JsonConvert.SerializeObject(new FailedModel
                    {
                        Errors = new List<string> { message }
                    }, Newtonsoft.Json.Formatting.Indented),

                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

        }

        public abstract IActionResult ProcessRequest(T record);

        public abstract IValidationResult IsValid(T record);
    }
}
