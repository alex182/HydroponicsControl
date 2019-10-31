using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.BackgroundJobs.Models
{
    public class HumidifierPressureAltitudeTemperatureJobOptions : IHumidifierPressureAltitudeTemperatureJobOptions
    {
        public int CheckInterval { get; set; }
        public int HumiditySensorGpio { get; set; }
        public int RelayGpio { get; set; }
        public int SclkPin { get; set; }
        public int TargetHumidity { get; set; }
        public double TargetTemperature { get; set; }
        public double TargetPressure { get; set; }
        public double TargetAltitude { get; set; }
    }
}
