using System;
using System.Xml.Serialization;

namespace SalesforceMagic.ORM.BaseRequestTemplates
{
    [Serializable]
    [XmlRoot("Envelope", Namespace = SalesforceNamespaces.Envelope)]
    public class XmlRequest
    {
        [XmlElement("Header", Namespace = SalesforceNamespaces.Envelope)]
        public XmlHeader Header { get; set; }

        [XmlElement("Body", Namespace = SalesforceNamespaces.Envelope)]
        public XmlBody Body { get; set; }
    }
}