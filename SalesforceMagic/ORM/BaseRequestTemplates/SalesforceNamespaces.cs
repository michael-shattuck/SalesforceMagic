using System.Xml;

namespace SalesforceMagic.ORM.BaseRequestTemplates
{
    public class SalesforceNamespaces
    {
        public const string JobInfo = "http://www.force.com/2009/06/asyncapi/dataload";
        public const string Envelope = "http://schemas.xmlsoap.org/soap/envelope/";
        public const string SalesforceRequest = "urn:partner.soap.sforce.com";
        public const string SObject = "urn:sobject.enterprise.soap.sforce.com";

        internal static XmlNamespaceManager GetSalesforceNamespace(XmlDocument document)
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(document.NameTable);
            manager.AddNamespace("sf", SObject);

            return manager;
        }
    }
}