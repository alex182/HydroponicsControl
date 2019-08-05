using SensorClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SensorClient.SensorReadings
{
    public interface ISensorReadingClientFactory
    {
        ISensorReadingClient Create(int gpioPin, SensorType sensorType);
    }
}
