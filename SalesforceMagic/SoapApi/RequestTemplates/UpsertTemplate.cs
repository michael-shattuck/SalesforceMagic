using System;
using System.Xml.Serialization;
using SalesforceMagic.Entities;
using SalesforceMagic.ORM.BaseRequestTemplates;

namespace SalesforceMagic.SoapApi.RequestTemplates
{
    [Serializable]
    public class UpsertTemplate
    {
        [XmlElement("ExternalIDFieldName", Namespace = SalesforceNamespaces.SalesforceRequest, Order = 1)]
        public string ExternalIdFieldName { get; set; }

        [XmlElement("sObjects", Namespace = SalesforceNamespaces.SalesforceRequest, Order = 2)]
        public SObject[] SObjects { get; set; }
    }
}   