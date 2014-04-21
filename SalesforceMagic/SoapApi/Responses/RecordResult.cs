using SalesforceMagic.Attributes;

namespace SalesforceMagic.SoapApi.Responses
{
    public class RecordResult
    {
        [SalesforceName("id")]
        public string Id { get; set; }

        [SalesforceName("success")]
        public bool Success { get; set; }

        [SalesforceName("message")]
        public string Message { get; set; }
    }
}