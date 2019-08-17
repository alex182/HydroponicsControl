using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.BackgroundJobs.Models
{
    public class HumidifierJobOptions : IHumidifierJobOptions
    {
        public int CheckInterval { get; set; }
        public int TargetHumidity { get; set; }
        public int RelayGpio { get; set; }
        public int HumiditySensorGpio { get; set; }
    }
}
