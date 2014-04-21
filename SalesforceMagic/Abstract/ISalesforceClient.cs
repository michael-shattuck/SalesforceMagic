using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SalesforceMagic.BulkApi.Configuration;
using SalesforceMagic.BulkApi.Models;
using SalesforceMagic.Configuration;
using SalesforceMagic.Entities;

namespace SalesforceMagic.Abstract
{
    public interface ISalesforceClient : IDisposable
    {
        #region Utility Methods

        void ChangeEnvironment(SalesforceConfig config, bool login = false);
        string GetSessionId();
        SalesforceSession Login();

        #endregion

        #region Query Methods

        IEnumerable<T> Query<T>(Expression<Func<T, bool>> predicate, int limit = 0) where T : SObject;
        IEnumerable<T> Query<T>(string query);
        T QuerySingle<T>(Expression<Func<T, bool>> predicate) where T : SObject;
        T QuerySingle<T>(string query);

        #endregion

        #region Crud Methods

        SalesforceResponse Crud<T>(CrudOperation<T> operation) where T : SObject;
        SalesforceResponse Insert<T>(IEnumerable<T> items) where T : SObject;
        SalesforceResponse Insert<T>(T item) where T : SObject;
        SalesforceResponse Upsert<T>(IEnumerable<T> items, string externalIdField) where T : SObject;
        SalesforceResponse Upsert<T>(T item, string externalIdField) where T : SObject;
        SalesforceResponse Update<T>(IEnumerable<T> items) where T : SObject;
        SalesforceResponse Update<T>(T item) where T : SObject;
        SalesforceResponse Delete<T>(IEnumerable<T> items) where T : SObject;
        SalesforceResponse Delete<T>(T item) where T : SObject;

        #endregion

        #region Bulk Data Methods

        JobInfo CreateBulkJob<T>(JobConfig config);
        BatchInfo AddBatch<T>(IEnumerable<T> items, string jobId);
        SalesforceResponse CloseBulkJob(string jobId);

        #endregion

    }
}