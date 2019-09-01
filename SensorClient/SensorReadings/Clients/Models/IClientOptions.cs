using System;
using System.Collections.Generic;
using System.Text;

namespace SensorClient.SensorReadings.Clients.Models
{
    public interface IClientOptions
    {
        int GpioPin { get; set; }
    }
}
