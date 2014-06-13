using System;
using SalesforceMagic.Attributes;

namespace SalesforceMagic.BulkApi.Models
{
    [SalesforceName("batchInfo")]
    public class BatchInfo
    {
        [SalesforceName("id")]
        public string Id { get; set; }

        [SalesforceName("jobId")]
        public string JobId { get; set; }

        [SalesforceName("state")]
        public string State { get; set; }

        [SalesforceName("createdDate")]
        public DateTime CreatedDate { get; set; }

        [SalesforceName("numberRecordsProcessed")]
        public int NumberRecordsProcessed { get; set; }
    }
}