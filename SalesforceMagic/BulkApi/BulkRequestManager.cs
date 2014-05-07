using System;
using SalesforceMagic.BulkApi.Configuration;
using SalesforceMagic.Configuration;
using SalesforceMagic.Http.Enums;
using SalesforceMagic.Http.Models;

namespace SalesforceMagic.BulkApi
{
    internal static class BulkRequestManager
    {
        internal static string BulkApiUrl = "/services/async/24.0/job"; // TODO: Make version # dynamic or configurable

        public static HttpRequest GetStartJobRequest<T>(JobConfig config, SalesforceSession session)
        {
            HttpRequest request = new HttpRequest
            {
                Url = session.InstanceUrl + BulkApiUrl,
                Body = BulkCommands.CreateJob(config, typeof(T).Name),
                Method = RequestType.POST,
                ContentType = "application/xml"
            };
            request.Headers.Add("X-SFDC-Session", session.SessionId);

            return request;
        }

        public static HttpRequest GetCloseJobRequest(string jobId, SalesforceSession session)
        {
            HttpRequest request = new HttpRequest
            {
                Url = session.InstanceUrl + BulkApiUrl + "/" + jobId,
                Body = BulkCommands.CloseJob(),
                Method = RequestType.POST,
                ContentType = "application/xml"
            };
            request.Headers.Add("X-SFDC-Session", session.SessionId);

            return request;
        }

        public static HttpRequest GetBatchRequest<T>(T[] items, string jobId, SalesforceSession session)
        {
            HttpRequest request = new HttpRequest
            {
                Url = session.InstanceUrl + BulkApiUrl + "/" + jobId + "/batch",
                Body = BulkCommands.CreateBatch(items),
                Method = RequestType.POST,
                ContentType = "text/csv; charset=UTF-8"
            };
            request.Headers.Add("X-SFDC-Session", session.SessionId);

            return request;
        }
    }
}