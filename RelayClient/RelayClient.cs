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
        private List<Relay> _relays;

        public RelayClient(ILoggerFactory loggerFactory, 
            IGpioController gpioController,
            IRelayClientOptions relayClientOptions)
        {
            _logger = loggerFactory.CreateLogger<RelayClient>();
            _gpioController = gpioController;

            SetRelays(relayClientOptions.Relays);
        }

        private void SetRelays(List<Relay> relays)
        {
            foreach(var relay in relays)
            {
                _gpioController.OpenPin(relay.GpioPin);
            }

            _relays = relays;
        }

        public List<Relay> GetRelays()
        {
            return _relays;
        }

        public RelayState? GetRelayState(int gpioPin)
        {
            var IsPinOpen = _gpioController.IsPinOpen(gpioPin);

            if (!IsPinOpen)
                _gpioController.OpenPin(gpioPin);

            //_gpioController.SetPinMode(gpioPin, PinMode.Input);
            var pinState = _gpioController.GetPinMode(gpioPin);

            return pinState == PinMode.Output ? RelayState.On : RelayState.Off;
        }

        public ToggleRelayStateResponse ToggleRelayState(ToggleRelayStateRequest request)
        {
            var response = new ToggleRelayStateResponse
            {
                GpioPin = request.GpioPin,
                State =request.State
            };

            var pinValue = request.State == RelayState.On ? PinValue.High: PinValue.Low;

            if(pinValue == PinValue.High)
                _gpioController.SetPinMode(request.GpioPin, PinMode.Output);
            else
                _gpioController.SetPinMode(request.GpioPin, PinMode.Input);

            return response; 
        }

    }
}
