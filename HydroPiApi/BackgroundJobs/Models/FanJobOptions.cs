using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.BackgroundJobs.Models
{
    public class FanJobOptions : IFanJobOptions
    {
        public int RunInterval { get; set; }
        public int RunDuration { get; set; }
        public int RelayGpioPin { get; set; }
    }
}
