using System;
using System.Xml.Serialization;
using SalesforceMagic.ORM.BaseRequestTemplates;

namespace SalesforceMagic.SoapApi.RequestTemplates
{
    [Serializable]
    public class QueryMoreTemplate
    {
        public QueryMoreTemplate()
        {
        }

        public QueryMoreTemplate(string queryLocator)
        {
            QueryLocator = queryLocator;
        }

        [XmlElement("queryLocator", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public string QueryLocator { get; set; }
    }
}