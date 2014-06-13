using System;
using System.Xml.Serialization;
using SalesforceMagic.ORM.BaseRequestTemplates;

namespace SalesforceMagic.BulkApi.RequestTemplates
{
    [Serializable]
    [XmlRoot("jobInfo", Namespace = SalesforceNamespaces.JobInfo)]
    public class JobTemplate
    {
        [XmlElement("operation")] // TODO: Move to enum?
        public string Operation { get; set; }

        [XmlElement("object")]
        public string Object { get; set; }

        [XmlElement("externalIdFieldName")]
        public string ExternalIdFieldName { get; set; }

        [XmlElement("concurrencyMode")]
        public string ConcurrencyMode { get; set; }

        [XmlElement("contentType")]
        public string ContentType { get; set; }

        [XmlElement("state")] // TODO: Move to enum?
        public string State { get; set; }
    }
}