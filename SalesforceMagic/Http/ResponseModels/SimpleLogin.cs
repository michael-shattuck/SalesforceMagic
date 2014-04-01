using System.Security;
using SalesforceMagic.Attributes;

namespace SalesforceMagic.Http.ResponseModels
{
    public class SimpleLogin
    {
        [SalesforceName("sessionId")]
        public string SessionId { get; set; }

        [SalesforceName("serverUrl")]
        public string ServerUrl { get; set; }
    }
}