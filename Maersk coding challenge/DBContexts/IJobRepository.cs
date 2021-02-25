using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maersk_coding_challenge
{
    public interface IJobRepository
    {
        public Task<Job> GetJobAsync(int jobNo);
        public void AddJob(Job job);
        public bool Save();

        public IEnumerable<Job> GetJobs();

        public Task<IEnumerable<Job>> GetJobsAsync();
        void UpdateJobsAsync(List<Job> jobs);
    }
}
