using System;
using SensorClient.SensorReadings;
using SensorClient.Models;
using SensorClient.SensorReadings.Clients;

namespace SensorClient.SensorReadings
{
    public class SensorReadingClientFactory : ISensorReadingClientFactory
    {
        public ISensorReadingClient Create(int gpioPin, SensorType sensorType)
        {
            if(sensorType == SensorType.Humidity_Temperature)
            {
                return new HumidityTemperatureClient(gpioPin);
            }

            return null; 
        }
    }
}
