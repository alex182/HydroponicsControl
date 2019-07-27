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

        public ProcessorFactory(ILoggerFactory loggerFactory,
            IRelayClient relayClient)
        {
            _loggerFactory = loggerFactory;
            _relayClient = relayClient;
        }

        public IProcessor Create(IProcessorRequest request)
        {
            if (request is GetRelayStateProcessorRequestVersionOne)
                return new GetRelayStateProcessorVersionOne((GetRelayStateProcessorRequestVersionOne)request,
                    _loggerFactory,_relayClient);

            if (request is GetRelaysProcessorRequestVersionOne)
                return new GetRelaysProcessorVersionOne((GetRelaysProcessorRequestVersionOne)request,
                    _loggerFactory, _relayClient);


            return null;
        }
    }
}
