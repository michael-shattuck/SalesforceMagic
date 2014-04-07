using System;
using System.Xml.Serialization;

namespace SalesforceMagic.ORM.BaseRequestTemplates
{
    [Serializable]
    public class SessionHeader
    {
        [XmlElement("sessionId", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public string SessionId { get; set; } 
    }
}