using FluentValidation;
using HydroponicsControl.Controllers.Relay.Version1.Processors.Request;
using RelayClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroponicsControl.Controllers.Relay.Version1.Validators
{
    public class GetRelayStateRequestValidator: AbstractValidator<GetRelayStateProcessorRequestVersionOne>
    {
        public GetRelayStateRequestValidator(IRelayClient relayClient)
        {
            //TODO: check to see if relay actually exists
           // RuleFor(x => x.RelayID).ExclusiveBetween(1, int.MaxValue);
        }
    }
}
