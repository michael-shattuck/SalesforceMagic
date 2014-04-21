using System.Collections.Generic;
using SalesforceMagic.Entities;
using SalesforceMagic.SoapApi.Enum;

namespace SalesforceMagic.Configuration
{
    public class CrudOperation<T> where T : SObject
    {
        public CrudOperations OperationType { get; set; }
        public string ExternalIdField { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}