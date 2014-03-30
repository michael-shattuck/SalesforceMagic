using System;

namespace SalesforceMagic.Attributes
{
    public class SalesforceNameAttribute : Attribute
    {
        public string Name { get; set; }

        public SalesforceNameAttribute(string name)
        {
            Name = name;
        }
    }
}