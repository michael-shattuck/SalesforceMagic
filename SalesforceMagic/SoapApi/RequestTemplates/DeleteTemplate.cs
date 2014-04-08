using System;
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
        public SObject[] SObjects { get; set; }

        [XmlElement("ids", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public string Ids
        {
            get { return string.Join(",", SObjects.Select(x => x.Id)); }
            set { } // Apparently this is needed for XML serialization...
        }
    }
}