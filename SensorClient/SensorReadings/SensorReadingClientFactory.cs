using System;
using SensorClient.SensorReadings;
using SensorClient.Models;
using SensorClient.SensorReadings.Clients;
using SensorClient.SensorReadings.Clients.Models;

namespace SensorClient.SensorReadings
{
    public class SensorReadingClientFactory : ISensorReadingClientFactory
    {
        public ISensorReadingClient Create(IClientOptions options, SensorType sensorType)
        {
            if(sensorType == SensorType.Humidity_Temperature)
            {
                return new HumidityTemperatureClient((SensorReadingByGpioOptions)options);
            }

            if(sensorType == SensorType.Humidity_Temperature_Altitude_Pressure)
            {
                return new HumidityTemperatureAltitudePressureClient(
                    (SensorReadingByGpioI2COptions)options);
            }

            return null; 
        }
    }
}
