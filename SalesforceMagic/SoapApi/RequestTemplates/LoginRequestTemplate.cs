using System;
using System.Xml.Serialization;
using SalesforceMagic.ORM.BaseRequestTemplates;

namespace SalesforceMagic.SoapApi.RequestTemplates
{
    [Serializable]
    public class LoginRequestTemplate
    {
        public LoginRequestTemplate()
        {
        }

        public LoginRequestTemplate(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [XmlElement("username", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public string Username { get; set; }

        [XmlElement("password", Namespace = SalesforceNamespaces.SalesforceRequest)]
        public string Password { get; set; }
    }
}