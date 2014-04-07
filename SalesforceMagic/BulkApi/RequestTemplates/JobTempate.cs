using System;
using System.Xml.Serialization;

namespace SalesforceMagic.BulkApi.RequestTemplates
{
    [Serializable]
    public class JobTempate
    {
        [XmlElement("operation")] // TODO: Move to enum?
        public string Operation { get; set; }

        [XmlElement("object")]
        public string Object { get; set; }

        [XmlElement("contentType")]
        public string ContentType { get; set; }

        [XmlElement("state")] // TODO: Move to enum?
        public string State { get; set; }

        [XmlElement("concurrencyMode")]
        public string ConcurrencyMode { get; set; }
    }
}