using HydroPiApi.Controllers.Relay.Version1.Processors;
using HydroPiApi.Controllers.Relay.Version1.Processors.Request;
using HydroPiApi.Controllers.Sensors.Version1.Processors;
using HydroPiApi.Controllers.Sensors.Version1.Processors.Request;
using Microsoft.Extensions.Logging;
using RelayClient;
using SensorClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.Controllers.Common.Processor
{
    public class ProcessorFactory : IProcessorFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IRelayClient _relayClient;
        private readonly ISensorClient _sensorClient;

        public ProcessorFactory(ILoggerFactory loggerFactory,
            IRelayClient relayClient,
            ISensorClient sensorClient)
        {
            _loggerFactory = loggerFactory;
            _relayClient = relayClient;
            _sensorClient = sensorClient;
        }

        public IProcessor Create(IProcessorRequest request)
        {

            if (request is GetRelayStateProcessorRequestVersionOne)
                return new GetRelayStateProcessorVersionOne((GetRelayStateProcessorRequestVersionOne)request,
                    _loggerFactory,_relayClient);

            if (request is GetRelaysProcessorRequestVersionOne)
                return new GetRelaysProcessorVersionOne((GetRelaysProcessorRequestVersionOne)request,
                    _loggerFactory, _relayClient);

            if (request is ToggleRelayStateProcessorRequestVersionOne)
                return new ToggleRelayStateProcessorVersionOne((ToggleRelayStateProcessorRequestVersionOne)request,
                    _loggerFactory, _relayClient);

            if(request is GetSensorsProcessorRequestVersionOne)
                return new GetSensorsProcessorVersionOne((GetSensorsProcessorRequestVersionOne)request,
                    _loggerFactory, _sensorClient);

            if (request is GetSensorReadingProcessorRequestVersionOne)
                return new GetSensorReadingProcessorVersionOne((GetSensorReadingProcessorRequestVersionOne)request,
                    _loggerFactory, _sensorClient);

            return null;
        }
    }
}
