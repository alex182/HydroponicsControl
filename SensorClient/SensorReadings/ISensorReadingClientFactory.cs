using SensorClient.Models;
using SensorClient.SensorReadings.Clients.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SensorClient.SensorReadings
{
    public interface ISensorReadingClientFactory
    {
        ISensorReadingClient Create(IClientOptions options, SensorType sensorType);
    }
}
