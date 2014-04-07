using System.Collections.Generic;

namespace SalesforceMagic
{
    public class SalesforceResponse
    {
        public bool Success { get; set; }
        public IList<string> Errors { get; set; }
        public string Message { get; set; }
    }
}