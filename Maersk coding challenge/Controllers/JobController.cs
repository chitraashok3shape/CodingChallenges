using Maersk_coding_challenge.DBContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Maersk_coding_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        BackgroundWorker backgroundWorker = new BackgroundWorker();

        private readonly ILogger<JobController> _logger;
        private readonly IJobRepository _context;

        public JobController(ILogger<JobController> logger, IJobRepository context)
        {
            _logger = logger;
            _context = context;
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted; ;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var jobList = (List<Job>)e.Result;
            _context.UpdateJobsAsync(jobList);
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var jobArray = (int[])(e.Argument);
            if (jobArray != null)
            {
                Array.Sort(jobArray);
                var jobList = jobArray.Select(jobItem => new Job
                {
                    JobNo = jobItem,
                    JobStatus = Status.Pending,
                    TimeStamp = DateTime.Now
                }).ToList();
                //var jobList = (List<Job>)e.Argument;
                foreach (var job in jobList)
                    _context.AddJob(job);

                e.Result = jobList;
            }
        }

        [HttpGet("{jobNo}", Name = "GetJob")]
        public async Task<IActionResult> GetJob(int jobNo)
        {
            var jobRepo = await _context.GetJobAsync(jobNo);

            if (jobRepo == null)
            {
                return NotFound();
            }

            return Ok(jobRepo);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Job>>> EnqueueJob(int[] jobArray)
        {
            backgroundWorker.RunWorkerAsync(jobArray);
            _context.Save();

            return Ok(await _context.GetJobsAsync());
        }
    }
}
