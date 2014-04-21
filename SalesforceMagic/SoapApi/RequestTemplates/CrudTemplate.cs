using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using SalesforceMagic.Entities;
using SalesforceMagic.ORM.BaseRequestTemplates;

namespace SalesforceMagic.SoapApi.RequestTemplates
{
    [Serializable]
    public class BasicCrudTemplate
    {
        [XmlIgnore]
        public IEnumerable<SObject> SObjects { get; set; }

        [XmlElement("sObjects", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public List<SObject> Items {
            get
            {
                return SObjects.ToList();
            } 
        }
    }
}