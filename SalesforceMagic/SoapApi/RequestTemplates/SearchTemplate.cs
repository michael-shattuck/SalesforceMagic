using System;
using System.Xml.Serialization;
using SalesforceMagic.ORM.BaseRequestTemplates;

namespace SalesforceMagic.SoapApi.RequestTemplates
{
    [Serializable]
    public class SearchTemplate
    {
        public SearchTemplate()
        {
        }

        public SearchTemplate(string searchString)
        {
            SearchString = searchString;
        }

        [XmlElement("searchString", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public string SearchString { get; set; }
    }
}