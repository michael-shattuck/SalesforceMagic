using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using SalesforceMagic.Entities;
using SalesforceMagic.ORM.BaseRequestTemplates;

namespace SalesforceMagic.SoapApi.RequestTemplates
{
    [Serializable]
    public class UpsertTemplate
    {
        [XmlElement("ExternalIDFieldName", Namespace = SalesforceNamespaces.SalesforceRequest, Order = 1)]
        public string ExternalIdFieldName { get; set; }

        [XmlIgnore]
        public IEnumerable<SObject> SObjects { get; set; }

        [XmlElement("sObjects", Namespace = SalesforceNamespaces.SalesforceRequest, Order = 2)]
        public List<SObject> Items
        {
            get
            {
                return SObjects.ToList();
            }
        }
    }
}   