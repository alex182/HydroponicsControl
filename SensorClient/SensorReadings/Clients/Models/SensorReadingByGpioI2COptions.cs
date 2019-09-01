using System;
using System.Collections.Generic;
using System.Text;

namespace SensorClient.SensorReadings.Clients.Models
{
    public class SensorReadingByGpioI2COptions : ISensorReadingByGpioI2COptions
    {
        public int GpioPin { get; set; }
        public int I2CPin { get; set; }
    }
}
