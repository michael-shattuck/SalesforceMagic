using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SalesforceMagic.ORM.XmlRequestTemplates
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