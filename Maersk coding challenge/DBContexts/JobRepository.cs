using Maersk_coding_challenge.DBContexts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Maersk_coding_challenge
{
    public class JobRepository : IJobRepository
    {
        public JobContext _context { get; }

        public JobRepository(JobContext jobContext)
        {
            _context = jobContext;
        }

        public Task<Job> GetJobAsync(int jobNo)
        {
            var internalId = _context.JobEntity.Single(job => job.JobNo.Equals(jobNo)).JobNo;
            return Task.FromResult(_context.JobEntity.FindAsync(internalId).Result);
        }

        public void AddJob(Job job)
        {
            job.Duration = DateTime.Now - job.TimeStamp;
            _context.JobEntity.AddAsync(job);
        }

        public IEnumerable<Job> GetJobs()
        {
            return _context.JobEntity.ToList();
        }

        public Task<IEnumerable<Job>> GetJobsAsync()
        {
            return Task.FromResult<IEnumerable<Job>>(_context.JobEntity.ToList());
        }

        public void UpdateJobsAsync(List<Job> jobs)
        {
            foreach (var guid in jobs.Select(x => x.JobNo))
            {
                var matchingJob = _context.JobEntity.FindAsync(guid);
                matchingJob.Result.JobStatus = Status.Completed;
                matchingJob.Result.Duration = DateTime.Now - matchingJob.Result.TimeStamp;
            }

            _context.SaveChangesAsync();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}

