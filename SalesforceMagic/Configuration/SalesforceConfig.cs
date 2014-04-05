namespace SalesforceMagic.Configuration
{
    public class SalesforceConfig
    {
        public bool IsSandbox { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SecurityToken { get; set; }
        public string Environment { get; set; }
        public bool LogoutOnDisposal { get; set; }
    }
}