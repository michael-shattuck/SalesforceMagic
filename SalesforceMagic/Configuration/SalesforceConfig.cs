namespace SalesforceMagic.Configuration
{
    public class SalesforceConfig
    {
        internal string SoapUrl = "/services/Soap/u/24.0";
        public bool IsSandbox { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SecurityToken { get; set; }
        internal string InstanceUrl { get; set; }
        public string SessionId { get; set; }
    }
}