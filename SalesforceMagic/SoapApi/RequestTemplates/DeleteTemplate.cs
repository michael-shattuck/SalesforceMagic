using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using SalesforceMagic.Entities;
using SalesforceMagic.ORM.BaseRequestTemplates;

namespace SalesforceMagic.SoapApi.RequestTemplates
{
    [Serializable]
    public class DeleteTemplate
    {
        [XmlIgnore]
        public IEnumerable<SObject> SObjects { get; set; }

        [XmlElement("ids", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public string[] Ids
        {
            get { return SObjects.Select(x => x.Id).ToArray(); }
            set { } // Apparently this is needed for XML serialization...
        }
    }
}