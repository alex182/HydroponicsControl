using SensorClient.Models;
using SensorClient.Models.SensorReadings;
using SensorClient.SensorReadings.Clients.Models;
using System.Collections.Generic;

namespace SensorClient
{
    public interface ISensorClient
    {
        List<Sensor> GetSensors();
        ISensorReading GetSensorReading(IClientOptions options);
    }
}
