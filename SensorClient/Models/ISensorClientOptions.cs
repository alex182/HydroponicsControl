using System;
using System.Collections.Generic;
using System.Text;

namespace SensorClient.Models
{
    public interface ISensorClientOptions
    {
        List<Sensor> Sensors { get; set; }
    }
}
