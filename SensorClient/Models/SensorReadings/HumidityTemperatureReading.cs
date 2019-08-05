using Iot.Units;

namespace SensorClient.Models.SensorReadings
{
    public class HumidityTemperatureReading : ISensorReading
    {
        public Temperature Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
