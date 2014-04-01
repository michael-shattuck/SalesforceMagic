using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace SalesforceMagic.ORM.XmlRequestTemplates
{
    [Serializable]
    public class XmlBody
    {
        [XmlElement("login", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public LoginRequestTemplate LoginTemplate { get; set; }

        [XmlElement("query", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public QueryTemplate QueryTemplate { get; set; }
    }
}