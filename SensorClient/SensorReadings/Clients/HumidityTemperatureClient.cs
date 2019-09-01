using System;
using System.Collections.Generic;
using System.Text;
using Iot.Device.DHTxx;
using SensorClient.Models.SensorReadings;
using SensorClient.SensorReadings.Clients.Models;

namespace SensorClient.SensorReadings.Clients
{
    public class HumidityTemperatureClient : ISensorReadingClient
    {
        private readonly int _gpioPin; 

        public HumidityTemperatureClient(SensorReadingByGpioOptions options)
        {
            _gpioPin = options.GpioPin;
        }

        public ISensorReading ReadSensor()
        {
            var result = new HumidityTemperatureReading
            {
                Humidity = 0
            };

            using (Dht22 dht = new Dht22(_gpioPin))
            {
                //sometimes the sensor returns NaN's
                while(result.Humidity == 0)
                {
                    result.Temperature = dht.Temperature;
                    result.Humidity = dht.Humidity;

                    if(result.Humidity == 0)
                        System.Threading.Thread.Sleep(1000);
                }
            }

            return result;
        }
    }
}
