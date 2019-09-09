using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Text;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using SensorClient.Models.SensorReadings;
using SensorClient.SensorReadings.Clients.Models;

namespace SensorClient.SensorReadings.Clients
{
    public class HumidityTemperatureAltitudePressureClient : ISensorReadingClient
    {
        private readonly Bme280 _sensor; 
        const double defaultSeaLevelPressure = 1033.00;

        public HumidityTemperatureAltitudePressureClient(
            SensorReadingByGpioI2COptions options
            )
        {
            //Bme280.DefaultI2cAddress
            var i2cSettings = new I2cConnectionSettings(1, Bme280.DefaultI2cAddress);
            var i2cDevice = I2cDevice.Create(i2cSettings);
            _sensor = new Bme280(i2cDevice);
        }

        public ISensorReading ReadSensor()
        {
            var result = new HumidityTemperatureAltitudePressureReading();
            using (_sensor)
            {
                _sensor.SetPowerMode(Bmx280PowerMode.Forced);

                //set samplings
                _sensor.SetTemperatureSampling(Sampling.UltraLowPower);
                _sensor.SetPressureSampling(Sampling.UltraLowPower);
                _sensor.SetHumiditySampling(Sampling.UltraLowPower);

                result.Humidity = _sensor.ReadHumidityAsync().Result;
                result.Temperature = _sensor.ReadTemperatureAsync().Result;
                result.Altitude = _sensor.ReadAltitudeAsync(defaultSeaLevelPressure).Result;
                result.Pressure = _sensor.ReadPressureAsync().Result;
            }

            return result;
        }
    }
}
