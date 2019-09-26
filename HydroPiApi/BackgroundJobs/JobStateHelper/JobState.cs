using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydroPiApi.BackgroundJobs.JobStateHelper
{
    public class JobState : IJobState
    {
        public DateTimeOffset LastRunTime { get; set; }
        public DateTimeOffset NextRunTime { get; set; }
    }
}
