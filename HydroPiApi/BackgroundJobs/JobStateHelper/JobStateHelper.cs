using System.Collections.Generic;

namespace HydroPiApi.BackgroundJobs.JobStateHelper
{
    public static class JobStateHelper
    {
        private static Dictionary<string, IJobState> StateBag = new Dictionary<string, IJobState>();

        public static Dictionary<string, IJobState> GetJobs()
        {
            return StateBag; 
        }

        public static IJobState GetJobByName(string jobName)
        {
            IJobState job; 
            StateBag.TryGetValue(jobName.ToLowerInvariant().Trim(), out job);
            return job;
        }

        public static void AddOrUpdateJobState(IJobState jobState, string jobName)
        {
            var job = GetJobByName(jobName);

            if(job != null)
            {
                StateBag[jobName.ToLowerInvariant().Trim()] = jobState; 
            }
            else
            {
                StateBag.Add(jobName.ToLowerInvariant().Trim(), jobState); 
            }
        }
    }
}
