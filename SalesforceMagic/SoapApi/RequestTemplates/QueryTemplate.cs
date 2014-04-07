using System;
using System.Xml.Serialization;
using SalesforceMagic.ORM.BaseRequestTemplates;

namespace SalesforceMagic.SoapApi.RequestTemplates
{
    [Serializable]
    public class QueryTemplate
    {
        public QueryTemplate()
        {
        }

        public QueryTemplate(string queryString)
        {
            QueryString = queryString;
        }

        [XmlElement("queryString", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public string QueryString { get; set; }
    }
}