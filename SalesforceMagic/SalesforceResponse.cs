using System.Collections.Generic;
using SalesforceMagic.SoapApi.Responses;

namespace SalesforceMagic
{
    public class SalesforceResponse
    {
        public SalesforceResponse()
        {
            Errors = new List<string>();
            Results = new List<RecordResult>();
        }

        public bool Success { get; set; }
        public IList<string> Errors { get; set; }
        public IList<RecordResult> Results { get; set; }
        public RecordResult Result { get; set; } 
    }
}