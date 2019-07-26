using System;
using System.Collections.Generic;
using RelayClient.Models;
using System.Device.Gpio;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace RelayClient
{
    public class RelayClient : IRelayClient
    {
        //TODO: use logger once a log destination is decided on 
        private readonly ILogger _logger;
        private readonly IGpioController _gpioController;
        private readonly List<Relay> _relays;

        public RelayClient(ILoggerFactory loggerFactory, 
            IGpioController gpioController,
            IRelayClientOptions relayClientOptions)
        {
            _logger = loggerFactory.CreateLogger<RelayClient>();
            _gpioController = gpioController;
            _relays = relayClientOptions.Relays;
        }

        public List<Relay> GetRelays()
        {
            return _relays;
        }

        public Relay IsValidRelay(int relay)
        {
            return _relays.FirstOrDefault(r => r.Pin == relay);
        }

        public RelayState? GetRelayState(int relay)
        {
            if (IsValidRelay(relay) == null)
                return null;

            var pin = GetRelayPin(relay).Value;
            var IsPinOpen = _gpioController.IsPinOpen(pin);

            if (!IsPinOpen)
                return RelayState.Off;

            var pinState = _gpioController.Read(pin);

            return pinState == PinValue.High ? RelayState.On : RelayState.Off;
        }

        public ToggleRelayStateResponse ToggleRelayState(ToggleRelayStateRequest request)
        {
            var pinValue = request.State == RelayState.On ? PinValue.High: PinValue.Low;
            var response = new ToggleRelayStateResponse
            {
                Relay = request.Relay,
                State = null,
                IsSuccess = false,
                ErrorMessage = "Invalid relay"
            };

            var pin = GetRelayPin(request.Relay);
            if(pin == null)
            {
                return response; 
            }

            _gpioController.OpenPin(pin.Value);
            _gpioController.Write(pin.Value, pinValue);

            response.ErrorMessage = "";
            response.IsSuccess = true;

            return response; 
        }

        private int? GetRelayPin(int relay)
        {
            return _relays.FirstOrDefault(r => r.Pin == relay)?.Pin;
        }
    }
}
