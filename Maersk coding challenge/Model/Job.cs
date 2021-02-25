using System;
using System.ComponentModel.DataAnnotations;

namespace Maersk_coding_challenge
{
    public class Job
    {
        [Key]
        public int JobNo { get; set; }
        public DateTime TimeStamp { get; set; }

        public Guid Id { get; set; }

        public TimeSpan Duration { get; set; }

        public Status JobStatus { get; set; }

    }
}
