using System;
using System.Collections.Generic;
using System.Text;

namespace SensorClient.Models
{
    public class Sensor
    {
        public string Name { get; set; }
        public SensorType Type { get; set; }
        public int Pin { get; set; }
        public int GpioPin { get; set; }
        public int I2CPin { get; set; }
    }
}
