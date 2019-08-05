using System;
using System.Collections.Generic;
using System.Text;
using SensorClient.Models.SensorReadings;

namespace SensorClient.SensorReadings
{
    public interface ISensorReadingClient
    {
        ISensorReading ReadSensor();
    }
}
