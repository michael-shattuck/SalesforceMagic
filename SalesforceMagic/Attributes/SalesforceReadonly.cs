using System;
using SalesforceMagic.Enums;

namespace SalesforceMagic.Attributes
{
    /// <summary>
    ///     Used to specify a property that can be read
    ///     but never pushed to Salesforce
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SalesforceReadonly : Attribute
    {
//        public ApiType ApiType { get; set; }
//
//        public SalesforceReadonly(ApiType type = ApiType.Both)
//        {
//            ApiType = type;
//        }
    }
}