using System;
using SalesforceMagic.BulkApi.Enum;

namespace SalesforceMagic.BulkApi.Models
{
    public class JobInfo
    {
        public string Id { get; set; }
        public string Operation { get; set; }
        public string Object { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public JobState State { get; set; }
        public ConcurrencyMode ConcurrencyMode { get; set; }
        public string ContentType { get; set; }
        public int NumberBatchedQueued { get; set; }
        public int NumberBatchedInProgress { get; set; }
        public int NumberBatchedCompleted { get; set; }
        public int NumberBatchedFailed { get; set; }
        public int NumberBatchedTotal { get; set; }
        public int NumberBatchedProcessed { get; set; }
        public int NumberRetries { get; set; }
        public string ApiVersion { get; set; }
    }
}