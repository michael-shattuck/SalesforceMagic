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
        internal static string DefaultApiVersion = "24.0";
        internal static string SoapUrl = "services/Soap/u";

        internal static HttpRequest GetLoginRequest(SalesforceConfig config)
        {
            string domain = !string.IsNullOrEmpty(config.InstanceUrl)
                ? config.InstanceUrl
                : config.IsSandbox ? "https://test.salesforce.com" : "https://login.salesforce.com";

            string url = string.Format("{0}/{1}/{2}", domain, SoapUrl, !string.IsNullOrEmpty(config.ApiVersion) ? config.ApiVersion : DefaultApiVersion);
            HttpRequest request = new HttpRequest
            {
                Url = url,
                Body = SoapCommands.Login(config.Username, config.Password + config.SecurityToken),
                Method = RequestType.POST,
            };
            request.Headers.Add("SOAPAction", "login");

            return request;
        }

        internal static HttpRequest GetQueryRequest<T>(Expression<Func<T, bool>> predicate, int limit, SalesforceSession session) where T : SObject
        {
            string query = QueryBuilder.GenerateQuery(predicate, limit);
            return GetQueryRequest(query, session);
        }

        internal static HttpRequest GetQueryAllRequest<T>(int limit, SalesforceSession session) where T : SObject
        {
            string query = QueryBuilder.GenerateQuery<T>(limit);
            return GetQueryRequest(query, session);
        }

        internal static HttpRequest GetQueryRequest(string query, SalesforceSession session)
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

        internal static HttpRequest GetQueryMoreRequest(string queryLocator, SalesforceSession session)
        {
            HttpRequest request = new HttpRequest
            {
                Url = session.InstanceUrl + SoapUrl,
                Body = SoapCommands.QueryMore(queryLocator, session.SessionId),
                Method = RequestType.POST,
            };
            request.Headers.Add("SOAPAction", "queryMore");

            return request;
        }

        internal static HttpRequest GetCrudRequest<T>(CrudOperation<T> operation, SalesforceSession session) where T : SObject
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

        internal static HttpRequest GetCountRequest<T>(SalesforceSession session, Expression<Func<T, bool>> predicate = null)
        {
            string query = QueryBuilder.GenerateCountyQuery(predicate);
            return GetQueryRequest(query, session);
        }
    }
}