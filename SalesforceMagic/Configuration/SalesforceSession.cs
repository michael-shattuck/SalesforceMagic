using System;

namespace SalesforceMagic.Configuration
{
    public class SalesforceSession
    {
        public string SessionId { get; set; }
        public string InstanceUrl { get; set; }
        public string Environment { get; set; }
        public bool IsSandbox { get; set; }
        public string ApiVersion { get; set; }
        public DateTime LastLogin { get; set; }
    }
}