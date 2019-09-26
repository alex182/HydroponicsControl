using System;

namespace HydroPiApi.BackgroundJobs.JobStateHelper
{
    public interface IJobState
    {
        DateTimeOffset LastRunTime { get; set; }
        DateTimeOffset NextRunTime { get; set; }
    }
}