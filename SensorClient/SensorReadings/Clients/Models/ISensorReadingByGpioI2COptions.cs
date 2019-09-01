using System;
using System.Collections.Generic;
using System.Text;

namespace SensorClient.SensorReadings.Clients.Models
{
    public interface ISensorReadingByGpioI2COptions : IClientOptions
    {
        int GpioPin { get; set; }
        int I2CPin { get; set; }
    }
}
