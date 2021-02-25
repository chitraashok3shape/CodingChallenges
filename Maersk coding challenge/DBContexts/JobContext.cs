using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maersk_coding_challenge.DBContexts
{
    public class JobContext : DbContext
    {
        public DbSet<Job> JobEntity { get; set; }


        public JobContext(DbContextOptions<JobContext> options)
            : base(options)
        {

        }
    }
}
