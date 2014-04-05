using System.Collections.Generic;
using SalesforceMagic.ORM.XmlRequestTemplates;

namespace SalesforceMagic.ORM
{
    public static class SalesforceCommands
    {
        public static string Login(string username, string password)
        {
            return XmlRequestGenerator.GenerateRequest(new XmlBody
            {
                LoginTemplate = new LoginRequestTemplate(username, password)
            });
        }

        public static string Query(string query, string sessionId)
        {
            return XmlRequestGenerator.GenerateRequest(new XmlBody
            {
                QueryTemplate = new QueryTemplate(query)
            }, 
            new XmlHeader
            {
                SessionHeader = new SessionHeader
                {
                    SessionId = sessionId
                }
            });
        }

        public static string BulkInsert<T>(T[] items, string sessionId)
        {
            throw new System.NotImplementedException();
        }
    }
}
