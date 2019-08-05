using System;
using System.Device.Gpio;
using System.Threading;

namespace HydroPiApi.Controllers.Common
{
    public class MockGpioDriver : IGpioController
    {
        public int PinCount => 0;

        public void AddCallbackForPinValueChangedEvent(int pinNumber, PinEventTypes eventTypes, PinChangeEventHandler callback) { }

        public void ClosePin(int pinNumber) { }

        public int ConvertPinNumberToLogicalNumberingScheme(int pinNumber) => 0;

        public void Dispose() { }

        public PinMode GetPinMode(int pinNumber) => default;

        public bool IsPinModeSupported(int pinNumber, PinMode mode) => true;

        public bool IsPinOpen(int pinNumber) { return false; }

        public void OpenPin(int pinNumber) { }

        public void OpenPin(int pinNumber, PinMode mode) { }

        public PinValue Read(int pinNumber) => default;

        public void Read(Span<PinValuePair> pinValuePairs) { }

        public void RemoveCallbackForPinValueChangedEvent(int pinNumber, PinChangeEventHandler callback) { }

        public void SetPinMode(int pinNumber, PinMode mode) { }

        public WaitForEventResult WaitForEvent(int pinNumber, PinEventTypes eventTypes, CancellationToken cancellationToken) => default;

        public void Write(int pinNumber, PinValue value) { }

        public void Write(ReadOnlySpan<PinValuePair> pinValuePairs) { }
    }
}
