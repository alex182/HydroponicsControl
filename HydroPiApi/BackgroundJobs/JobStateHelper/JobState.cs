using System;
using HydroPiApi.BackgroundJobs.Models;

namespace HydroPiApi.BackgroundJobs.JobStateHelper
{
    public class JobState : IJobState
    {
        public DateTimeOffset LastRunTime { get; set; }
        public DateTimeOffset NextRunTime { get; set; }
        public IJobOptions JobOptions { get; set; }
    }
}
