using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using SalesforceMagic.Abstract;
using SalesforceMagic.Configuration;
using SalesforceMagic.Configuration.Abstract;
using SalesforceMagic.Entities;
using SalesforceMagic.Http;
using SalesforceMagic.Http.Models;
using SalesforceMagic.Http.ResponseModels;
using SalesforceMagic.ORM;

namespace SalesforceMagic
{
    public class SalesforceClient : ISalesforceClient, IDisposable
    {
        private readonly ISessionStoreProvider _sessionStore;
        private SalesforceConfig _config;

        public SalesforceClient(ISessionStoreProvider sessionStore, SalesforceConfig config, bool login = false)
        {
            _sessionStore = sessionStore;
            _config = config;
            if (login) Login();
        }

        public SalesforceClient(SalesforceConfig config, bool login = false)
        {
            _sessionStore = new MemoryCacheProvider();
            _config = config;
            if (login) Login();
        }

        public void ChangeEnvironment(SalesforceConfig config, bool login = false)
        {
            _config = config;
            if (login) Login();
        }

        public string GetSessionId()
        {
            return Login().SessionId;
        }

        public SalesforceSession Login()
        {
            SalesforceSession session = _sessionStore.RetrieveSession(_config.Environment ?? "Default");
            if (session != null) return session;

            using (HttpClient httpClient = new HttpClient())
            {
                XmlDocument response = httpClient.PerformRequest(RequestManager.GetLoginRequest(_config));
                SimpleLogin result = ResponseReader.ReadSimpleResponse<SimpleLogin>(response);

                Uri instanceUrl = new Uri(result.ServerUrl);
                session = new SalesforceSession
                {
                    InstanceUrl = instanceUrl.Scheme + "://" + instanceUrl.Host,
                    SessionId = result.SessionId
                };

                _sessionStore.StoreSession(session);

                return session;
            }
        }

        public T[] Query<T>(Expression<Func<T, bool>> predicate, int limit = 0) where T : SObject
        {
            using (HttpClient httpClient = new HttpClient())
            {
                XmlDocument response = httpClient.PerformRequest(RequestManager.GetQueryRequest(predicate, limit, Login()));
                T[] list = ResponseReader.ReadArrayResponse<T>(response);

                return list;
            }
        }

        public SalesforceResponse Insert<T>(T[] items)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                XmlDocument response = httpClient.PerformRequest(RequestManager.GetInsertRequest(items, Login()));
                SalesforceResponse salesforceResponse = ResponseReader.ReadSuccessResponse(response);

                return salesforceResponse;
            }
        }

        public SalesforceResponse Insert<T>(T item)
        {
            return PerformSimpleRequest(RequestManager.GetInsertRequest<T>(item, Login()));
        }

        public SalesforceResponse Upsert<T>(T[] items)
        {
            HttpRequest request = items.Count() < 100
                ? RequestManager.GetInsertRequest(items, Login())
                : RequestManager.GetBulkInsertRequest(items, Login());

            return PerformSimpleRequest(request);
        }

        public SalesforceResponse Upsert<T>(T item)
        {
            return PerformSimpleRequest(RequestManager.GetUpsertRequest(item, Login()));
        }

        public SalesforceResponse Update<T>(T[] items)
        {
            return PerformSimpleRequest(RequestManager.GetUpsertRequest(items, Login()));
        }

        public SalesforceResponse Update<T>(T item)
        {
            return PerformSimpleRequest(RequestManager.GetUpdateRequest(item, Login()));
        }

        public SalesforceResponse Delete<T>(T[] items)
        {
            return PerformSimpleRequest(RequestManager.GetUpdateRequest(items, Login()));
        }

        public SalesforceResponse Delete<T>(T item)
        {
            return PerformSimpleRequest(RequestManager.GetDeleteRequest(item, Login()));
        }

        public SalesforceResponse Delete<T>(string id)
        {
            return PerformSimpleRequest(RequestManager.GetDeleteRequest(id, Login()));
        }

        public SalesforceResponse Delete<T>(Expression<Func<T, bool>> predicate, int limit = 0) where T : SObject
        {
            return PerformSimpleRequest(RequestManager.GetDeleteRequest(predicate, Login()));
        }

        private SalesforceResponse PerformSimpleRequest(HttpRequest request)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                XmlDocument response = httpClient.PerformRequest(request);
                SalesforceResponse salesforceResponse = ResponseReader.ReadSuccessResponse(response);

                return salesforceResponse;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool safe)
        {
            if (safe && _config.LogoutOnDisposal)
            {
                // TODO: Logout
            }
        }

        ~SalesforceClient()
        {
            Dispose(false); // TODO: Logout anyway?
        }
    }
}