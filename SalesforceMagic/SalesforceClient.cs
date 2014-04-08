using System;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using SalesforceMagic.Abstract;
using SalesforceMagic.BulkApi;
using SalesforceMagic.BulkApi.Configuration;
using SalesforceMagic.BulkApi.Models;
using SalesforceMagic.Configuration;
using SalesforceMagic.Configuration.Abstract;
using SalesforceMagic.Entities;
using SalesforceMagic.Exceptions;
using SalesforceMagic.Http;
using SalesforceMagic.Http.Models;
using SalesforceMagic.Http.ResponseModels;
using SalesforceMagic.ORM;
using SalesforceMagic.SoapApi;
using SalesforceMagic.SoapApi.Enum;

namespace SalesforceMagic
{
    public class SalesforceClient : ISalesforceClient
    {
        #region Private Fields
        private readonly ISessionStoreProvider _sessionStore;
        private SalesforceConfig _config;
        #endregion

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

        public virtual void ChangeEnvironment(SalesforceConfig config, bool login = false)
        {
            _config = config;
            if (login) Login();
        }

        public virtual string GetSessionId()
        {
            return Login().SessionId;
        }

        public SalesforceSession Login()
        {
            SalesforceSession session;
            if (_config.UseSessionStore)
            {
                 session = _sessionStore.RetrieveSession(_config.Environment ?? "Default");
                if (session != null) return session;
            }

            using (HttpClient httpClient = new HttpClient())
            {
                XmlDocument response = httpClient.PerformRequest(SoapRequestManager.GetLoginRequest(_config));
                SimpleLogin result = ResponseReader.ReadSimpleResponse<SimpleLogin>(response);

                Uri instanceUrl = new Uri(result.ServerUrl);
                session = new SalesforceSession
                {
                    InstanceUrl = instanceUrl.Scheme + "://" + instanceUrl.Host,
                    SessionId = result.SessionId
                };

                if (_config.UseSessionStore)  _sessionStore.StoreSession(session);

                return session;
            }
        }

        public virtual T[] Query<T>(Expression<Func<T, bool>> predicate, int limit = 0) where T : SObject
        {
            return PerformArrayRequest<T>(SoapRequestManager.GetQueryRequest(predicate, limit, Login()));
        }

        public virtual T[] Query<T>(string query)
        {
            // TODO: Validate query
            return PerformArrayRequest<T>(SoapRequestManager.GetQueryRequest(query, Login()));
        }

        public virtual T QuerySingle<T>(Expression<Func<T, bool>> predicate) where T : SObject
        {
            return Query(predicate).FirstOrDefault();
        }

        public virtual T QuerySingle<T>(string query)
        {
            return Query<T>(query).FirstOrDefault();
        }

        public virtual SalesforceResponse Crud<T>(CrudOperation operation) where T : SObject
        {
            if (operation.Items.Length > 200)
                throw new SalesforceRequestException("The SOAP api only allows the modification of up tp 200 records. " +
                                                     "Please use the Bulk api methods for any operation that requires " +
                                                     "higher limits.");

            if (operation.OperationType == CrudOperations.Upsert && string.IsNullOrEmpty(operation.ExternalIdField))
                throw new SalesforceRequestException("Upsert requests required an external ID name field to be specified.");

            return PerformRequest<SalesforceResponse>(SoapRequestManager.GetCrudRequest(operation, Login()));
        }

        public virtual SalesforceResponse Insert<T>(T[] items) where T : SObject
        {
            return Crud<T>(new CrudOperation
            {
                Items = items,
                OperationType = CrudOperations.Insert
            });
        }

        public virtual SalesforceResponse Insert<T>(T item) where T : SObject
        {
            return Insert(new [] {item});
        }

        public virtual SalesforceResponse Upsert<T>(T[] items, string externalIdField) where T : SObject
        {
            return Crud<T>(new CrudOperation
            {
                Items = items,
                OperationType = CrudOperations.Upsert,
                ExternalIdField = externalIdField
            });
        }

        public virtual SalesforceResponse Upsert<T>(T item, string externalIdField) where T : SObject
        {
            return Upsert(new[] { item }, externalIdField);
        }

        public virtual SalesforceResponse Update<T>(T[] items) where T : SObject
        {
            return Crud<T>(new CrudOperation
            {
                Items = items,
                OperationType = CrudOperations.Update
            });
        }

        public virtual SalesforceResponse Update<T>(T item) where T : SObject
        {
            return Update(new[] { item });
        }

        public virtual SalesforceResponse Delete<T>(T[] items) where T : SObject
        {
            return Crud<T>(new CrudOperation
            {
                Items = items,
                OperationType = CrudOperations.Delete
            });
        }

        public virtual SalesforceResponse Delete<T>(T item) where T : SObject
        {
            return Delete(new[] { item });
        }

        public virtual JobInfo CreateBulkJob<T>(JobConfig config)
        {
            return PerformRequest<JobInfo>(BulkRequestManager.GetStartJobRequest<T>(config, Login()));
        }

        public virtual BatchInfo AddBatch<T>(T[] items, string jobId)
        {
            return PerformRequest<BatchInfo>(BulkRequestManager.GetBatchRequest(items, jobId, Login()));
        }

        public virtual SalesforceResponse CloseBulkJob(string jobId)
        {
            return PerformRequest<SalesforceResponse>(BulkRequestManager.GetCloseJobRequest(jobId, Login()));
        }

        #region Private Methods

        private T PerformRequest<T>(HttpRequest request)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                XmlDocument response = httpClient.PerformRequest(request);
                return ResponseReader.ReadSimpleResponse<T>(response);
            }
        }

        private T[] PerformArrayRequest<T>(HttpRequest request)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                XmlDocument response = httpClient.PerformRequest(request);
                return ResponseReader.ReadArrayResponse<T>(response);
            }
        }

        #endregion

        #region Implementation of IDisposable
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
        #endregion
    }
}