using System;
using System.Xml.Serialization;
using SalesforceMagic.ORM.BaseRequestTemplates;

namespace SalesforceMagic.SoapApi.RequestTemplates
{
    [Serializable]
    public class DeleteTemplate
    {
        [XmlElement("ids", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public string[] Ids { get; set; }
    }
}