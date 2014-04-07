using System;
using LINQtoCSV;

namespace SalesforceMagic.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SalesforceNameAttribute : CsvColumnAttribute
    {
        public SalesforceNameAttribute(string name)
        {
            Name = name;
        }
    }
}