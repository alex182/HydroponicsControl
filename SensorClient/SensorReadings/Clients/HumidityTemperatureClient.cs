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
            var attempts = 0; 
            while (attempts < 5)
            {
                using (Dht11 dht = new Dht11(_gpioPin))
                {
                    result.Temperature = dht.Temperature;
                    result.Humidity = dht.Humidity;
                }

                if(result.Temperature.Fahrenheit >= -40)
                {
                    break;
                }
                attempts++; 
            }
            return result;
        }
    }
}
