using System;
using System.Xml.Serialization;

namespace SalesforceMagic.ORM.XmlRequestTemplates
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