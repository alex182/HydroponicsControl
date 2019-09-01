using System;
using System.Collections.Generic;
using System.Text;

namespace SensorClient.SensorReadings.Clients.Models
{
    public class SensorReadingByGpioOptions : ISensorReadingByGpioOptions
    {
        public int GpioPin { get; set; }
    }
}
