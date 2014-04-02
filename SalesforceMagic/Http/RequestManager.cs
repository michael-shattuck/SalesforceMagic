using System;
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
        internal static HttpRequest GetLoginRequest(SalesforceConfig config, bool securityToken = false)
        {
            string url = config.IsSandbox ? "https://test.salesforce.com" : "https://login.salesforce.com";
            HttpRequest request = new HttpRequest
            {
                Url = url + config.SoapUrl,
                Body = SalesforceCommands.Login(config.Username, securityToken 
                    ? config.Password + config.SecurityToken 
                    : config.Password),
                Method = RequestType.POST,
            };
            request.Headers.Add("SOAPAction", "login");

            return request;
        }

        public static HttpRequest GetQueryRequest<T>(Expression<Func<T, bool>> predicate, int limit, SalesforceConfig config) where T : SObject
        {
            string query = QueryBuilder.GenerateQuery(predicate, limit);
            HttpRequest request = new HttpRequest
            {
                Url = config.InstanceUrl + config.SoapUrl,
                Body = SalesforceCommands.Query(query, config.SessionId),
                Method = RequestType.POST,
            };
            request.Headers.Add("SOAPAction", "query");

            return request;
        }
    }
}