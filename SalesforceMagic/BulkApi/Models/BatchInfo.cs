using System;

namespace SalesforceMagic.BulkApi.Models
{
    public class BatchInfo
    {
        public string Id { get; set; }
        public string JobId { get; set; }
        public string State { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NumberRecordsProcessed { get; set; }
    }
}