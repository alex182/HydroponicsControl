using Iot.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace SensorClient.Models.SensorReadings
{
    public class HumidityTemperatureAltitudePressureReading : ISensorReading
    {
        public double? Humidity { get; set; }
        public Temperature? Temperature { get; set; }
        public double? Altitude { get; set; }
        public double? Pressure { get; set; }
    }
}
