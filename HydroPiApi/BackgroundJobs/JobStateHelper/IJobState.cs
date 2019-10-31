using HydroPiApi.BackgroundJobs.Models;
using System;

namespace HydroPiApi.BackgroundJobs.JobStateHelper
{
    public interface IJobState
    {
        DateTimeOffset LastRunTime { get; set; }
        DateTimeOffset NextRunTime { get; set; }
        IJobOptions JobOptions { get; set; }
    }
}