using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using SensorClient.Models;
using SensorClient.Models.SensorReadings;
using SensorClient.SensorReadings;
using SensorClient.SensorReadings.Clients.Models;

namespace SensorClient
{
    public class SensorClient : ISensorClient
    {
        //TODO: use logger once a log destination is decided on 
        private readonly ILogger _logger;
        private readonly IGpioController _gpioController;
        private List<Sensor> _sensors;
        private ISensorReadingClientFactory _sensorReadingClientFactory; 

        public SensorClient(ILoggerFactory loggerFactory,
            IGpioController gpioController,
            ISensorClientOptions sensorClientOptions,
            ISensorReadingClientFactory sensorReadingClientFactory)
        {
            _gpioController = gpioController;
            _logger = loggerFactory.CreateLogger<SensorClient>();
            _sensorReadingClientFactory = sensorReadingClientFactory;
            _sensors = sensorClientOptions.Sensors;
        }       

        public ISensorReading GetSensorReading(IClientOptions options)
        {
            var currentSensor = GetSensorByGpio(options.GpioPin);
            var client = _sensorReadingClientFactory.Create(options, currentSensor.Type);

            return client.ReadSensor();
        }

        public List<Sensor> GetSensors()
        {
            return _sensors;
        }

        private Sensor GetSensorByGpio(int gpioPin)
        {
            return _sensors.First(s => s.GpioPin == gpioPin);
        }
    }
}
