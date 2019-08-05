using SensorClient.Models;
using SensorClient.Models.SensorReadings;
using System.Collections.Generic;

namespace SensorClient
{
    public interface ISensorClient
    {
        List<Sensor> GetSensors();
        ISensorReading GetSensorReading(int gpioPin);
    }
}
