using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.BackgroundJobs.Models
{
    public interface IFanJobOptions : IJobOptions
    {
        int JobInterval { get; set; }
        int RunDuration { get; set; }
        int RelayGpioPin { get; set; }
    }
}
