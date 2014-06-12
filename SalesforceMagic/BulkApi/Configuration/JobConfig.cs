using SalesforceMagic.BulkApi.Enum;

namespace SalesforceMagic.BulkApi.Configuration
{
    public class JobConfig
    {
        public ConcurrencyMode ConcurrencyMode { get; set; }
        public BulkOperations Operation { get; set; }
        public string ExternalIdFieldName { get; set; }
    }
}