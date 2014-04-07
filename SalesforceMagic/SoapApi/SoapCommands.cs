using SalesforceMagic.Entities;
using SalesforceMagic.Http;
using SalesforceMagic.ORM.BaseRequestTemplates;
using SalesforceMagic.SoapApi.Enum;
using SalesforceMagic.SoapApi.RequestTemplates;

namespace SalesforceMagic.SoapApi
{
    internal static class SoapCommands
    {
        internal static string Login(string username, string password)
        {
            return XmlRequestGenerator.GenerateRequest(new XmlBody
            {
                LoginTemplate = new LoginRequestTemplate(username, password)
            });
        }

        internal static string Query(string query, string sessionId)
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

        public static string CrudOperation(SObject[] items, CrudOperations operation, string sessionId)
        {
            XmlBody body = GetCrudBody(items, operation);
            return XmlRequestGenerator.GenerateRequest(body, new XmlHeader
            {
                SessionHeader = new SessionHeader
                {
                    SessionId = sessionId
                }
            });
        }

        private static XmlBody GetCrudBody(SObject[] items, CrudOperations operation)
        {
            XmlBody body = new XmlBody();

            switch (operation)
            {
                case CrudOperations.Insert:
                    body.InsertTemplate = new CrudTemplate
                    {
                        SObjects = items
                    };
                    break;
                case CrudOperations.Upsert:
                    body.UpsertTemplate = new CrudTemplate
                    {
                        SObjects = items
                    };
                    break;
                case CrudOperations.Update:
                    body.UpdateTemplate = new CrudTemplate
                    {
                        SObjects = items
                    };
                    break;
                case CrudOperations.Delete:
                    body.DeleteTemplate = new CrudTemplate
                    {
                        SObjects = items
                    };
                    break;
            }

            return body;
        }
    }
}
