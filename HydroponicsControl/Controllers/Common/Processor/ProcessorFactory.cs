using FluentValidation;
using HydroponicsControl.Controllers.Relay.Version1.Processors;
using HydroponicsControl.Controllers.Relay.Version1.Processors.Request;
using Microsoft.Extensions.Logging;
using RelayClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroponicsControl.Controllers.Common.Processor
{
    public class ProcessorFactory : IProcessorFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IRelayClient _relayClient;
        private readonly IValidator _validator;

        public ProcessorFactory(ILoggerFactory loggerFactory,
            IRelayClient relayClient,
            IValidator validator)
        {
            _loggerFactory = loggerFactory;
            _relayClient = relayClient;
            _validator = validator;
        }

        public IProcessor Create(IProcessorRequest request)
        {
            if (request is GetRelayStateProcessorRequestVersionOne)
                return new GetRelayStateProcessorVersionOne((GetRelayStateProcessorRequestVersionOne)request,
                    _loggerFactory,_relayClient,_validator);

            return null;
        }
    }
}
