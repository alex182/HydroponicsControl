using System;
using System.Collections.Generic;
using System.Text;

namespace SensorClient.Models
{
    public class SensorClientOptions : ISensorClientOptions
    {
        public List<Sensor> Sensors { get; set; } = new List<Sensor>(); 
    }
}
