using System;
using System.Linq.Expressions;
using SalesforceMagic.Configuration;
using SalesforceMagic.Entities;
using SalesforceMagic.Http.Enums;
using SalesforceMagic.Http.Models;
using SalesforceMagic.ORM;

namespace SalesforceMagic.SoapApi
{
    internal class SoapRequestManager
    {
        internal static string SoapUrl = "/services/Soap/u/24.0"; // TODO: Make version # dynamic or configurable

        internal static HttpRequest GetLoginRequest(SalesforceConfig config)
        {
            string url = config.IsSandbox ? "https://test.salesforce.com" : "https://login.salesforce.com";
            HttpRequest request = new HttpRequest
            {
                Url = url + SoapUrl,
                Body = SoapCommands.Login(config.Username, config.Password + config.SecurityToken),
                Method = RequestType.POST,
            };
            request.Headers.Add("SOAPAction", "login");

            return request;
        }

        public static HttpRequest GetQueryRequest<T>(Expression<Func<T, bool>> predicate, int limit, SalesforceSession session) where T : SObject
        {
            string query = QueryBuilder.GenerateQuery(predicate, limit);
            return GetQueryRequest(query, session);
        }

        public static HttpRequest GetQueryRequest(string query, SalesforceSession session)
        {
            HttpRequest request = new HttpRequest
            {
                Url = session.InstanceUrl + SoapUrl,
                Body = SoapCommands.Query(query, session.SessionId),
                Method = RequestType.POST,
            };
            request.Headers.Add("SOAPAction", "query");

            return request;
        }

        public static HttpRequest GetCrudRequest<T>(CrudOperation<T> operation, SalesforceSession session) where T : SObject
        {
            string body = SoapCommands.CrudOperation(operation, session.SessionId);
            HttpRequest request = new HttpRequest
            {
                Url = session.InstanceUrl + SoapUrl,
                Body = body,
                Method = RequestType.POST,
            };
            request.Headers.Add("SOAPAction", operation.OperationType.ToString().ToLower());

            return request;
        }
    }
}