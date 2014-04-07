using System;
using System.Xml.Serialization;
using SalesforceMagic.Entities;
using SalesforceMagic.ORM.BaseRequestTemplates;

namespace SalesforceMagic.SoapApi.RequestTemplates
{
    [Serializable]
    public class CrudTemplate
    {
        [XmlElement("sObjects", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public SObject[] SObjects { get; set; }
    }
}