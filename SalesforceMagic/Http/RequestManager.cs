using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SalesforceMagic.Configuration;
using SalesforceMagic.Entities;
using SalesforceMagic.Http.Enums;
using SalesforceMagic.Http.Models;
using SalesforceMagic.ORM;

namespace SalesforceMagic.Http
{
    internal static class RequestManager
    {
        internal static string SoapUrl = "/services/Soap/u/24.0";
        internal static string BulkApiUrl = "/services/async/24.0/job";

        internal static HttpRequest GetLoginRequest(SalesforceConfig config)
        {
            string url = config.IsSandbox ? "https://test.salesforce.com" : "https://login.salesforce.com";
            HttpRequest request = new HttpRequest
            {
                Url = url + SoapUrl,
                Body = SalesforceCommands.Login(config.Username, config.Password + config.SecurityToken),
                Method = RequestType.POST,
            };
            request.Headers.Add("SOAPAction", "login");

            return request;
        }

        public static HttpRequest GetQueryRequest<T>(Expression<Func<T, bool>> predicate, int limit, SalesforceSession session) where T : SObject
        {
            string query = QueryBuilder.GenerateQuery(predicate, limit);
            HttpRequest request = new HttpRequest
            {
                Url = session.InstanceUrl + SoapUrl,
                Body = SalesforceCommands.Query(query, session.SessionId),
                Method = RequestType.POST,
            };
            request.Headers.Add("SOAPAction", "query");

            return request;
        }

        public static HttpRequest GetBulkInsertRequest<T>(T[] items, SalesforceSession session)
        {
            HttpRequest request = new HttpRequest
            {
                Url = session.InstanceUrl + BulkApiUrl,
                Body = SalesforceCommands.BulkInsert(items, session.SessionId),
                Method = RequestType.POST,
            };
            request.Headers.Add("X-SFDC-Session", session.SessionId);

            return request;
        }

        public static HttpRequest GetInsertRequest<T>(T[] items, SalesforceSession login)
        {
            throw new NotImplementedException();
        }

        public static HttpRequest GetInsertRequest<T>(T items, SalesforceSession login)
        {
            throw new NotImplementedException();
        }
    }
}