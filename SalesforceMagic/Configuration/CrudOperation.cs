using SalesforceMagic.Entities;
using SalesforceMagic.SoapApi.Enum;

namespace SalesforceMagic.Configuration
{
    public class CrudOperation
    {
        public CrudOperations OperationType { get; set; }
        public string ExternalIdField { get; set; }
        public SObject[] Items { get; set; }
    }
}