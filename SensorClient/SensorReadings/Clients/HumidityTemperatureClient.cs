using System;
using System.Collections.Generic;
using System.Text;
using Iot.Device.DHTxx;
using SensorClient.Models.SensorReadings;

namespace SensorClient.SensorReadings.Clients
{
    public class HumidityTemperatureClient : ISensorReadingClient
    {
        private readonly int _gpioPin; 

        public HumidityTemperatureClient(int gpioPin)
        {
            _gpioPin = gpioPin;
        }

        public ISensorReading ReadSensor()
        {
            var result = new HumidityTemperatureReading();
            using (Dht22 dht = new Dht22(_gpioPin))
            {
                result.Temperature = dht.Temperature;
                result.Humidity = dht.Humidity;
            }

            return result;
        }
    }
}
