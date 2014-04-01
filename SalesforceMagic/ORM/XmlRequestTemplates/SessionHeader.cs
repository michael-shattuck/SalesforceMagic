using System;
using System.Xml.Serialization;

namespace SalesforceMagic.ORM.XmlRequestTemplates
{
    [Serializable]
    public class SessionHeader
    {
        [XmlElement("sessionId", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public string SessionId { get; set; } 
    }
}