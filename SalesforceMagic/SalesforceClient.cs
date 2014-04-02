using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml;
using SalesforceMagic.Configuration;
using SalesforceMagic.Entities;
using SalesforceMagic.Http;
using SalesforceMagic.Http.ResponseModels;
using SalesforceMagic.ORM;

namespace SalesforceMagic
{
    public class SalesforceClient : IDisposable
    {
        private SalesforceConfig _config;

        public SalesforceClient(SalesforceConfig config, bool login = false)
        {
            _config = config;
            if (login) _config.SessionId = Login();
        }

        public void ChangeEnvironment(SalesforceConfig config, bool login = false)
        {
            _config = config;
            if (login) _config.SessionId = Login();
        }

        public string GetSessionId()
        {
            return _config.SessionId ?? Login();
        }

        public string Login()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                XmlDocument response;
                SimpleLogin result;

                // Fallback for security token
                try
                {
                    response = httpClient.PerformRequest(RequestManager.GetLoginRequest(_config));
                    result = ResponseReader.ReadSimpleResponse<SimpleLogin>(response);
                }
                catch (Exception)
                {
                    response = httpClient.PerformRequest(RequestManager.GetLoginRequest(_config, true));
                    result = ResponseReader.ReadSimpleResponse<SimpleLogin>(response);
                }

                Uri instanceUrl = new Uri(result.ServerUrl);
                _config.InstanceUrl = instanceUrl.Scheme + "://" + instanceUrl.Host;
                _config.SessionId = result.SessionId;

                return _config.SessionId;
            }
        }

        public IEnumerable<T> Query<T>(Expression<Func<T, bool>> predicate, int limit = 0) where T : SObject
        {
            if (_config.SessionId == null) Login();
            using (HttpClient httpClient = new HttpClient())
            {
                XmlDocument response = httpClient.PerformRequest(RequestManager.GetQueryRequest(predicate, limit, _config));
                IEnumerable<T> list = ResponseReader.ReadArrayResponse<T>(response);

                return list;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool safe)
        {
            if (safe)
            {
                // TODO: Logout
            }
        }

        ~SalesforceClient()
        {
            Dispose(false);
        }
    }
}