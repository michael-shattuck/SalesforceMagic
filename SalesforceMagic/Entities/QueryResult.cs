using System.Collections.Generic;

namespace SalesforceMagic.Entities
{
    public class QueryResult<T>
    {
        public string QueryLocator { get; set; }
        public bool Done { get; set; }
        public IEnumerable<T> Records { get; set; }
    }
}