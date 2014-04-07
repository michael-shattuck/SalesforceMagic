using System;
using System.Linq.Expressions;
using SalesforceMagic.BulkApi.Configuration;
using SalesforceMagic.BulkApi.Models;
using SalesforceMagic.Configuration;
using SalesforceMagic.Entities;
using SalesforceMagic.SoapApi.Enum;

namespace SalesforceMagic.Abstract
{
    public interface ISalesforceClient : IDisposable
    {
        void ChangeEnvironment(SalesforceConfig config, bool login = false);
        string GetSessionId();
        SalesforceSession Login();
        T[] Query<T>(Expression<Func<T, bool>> predicate, int limit = 0) where T : SObject;
        SalesforceResponse PerformCrudOperation<T>(T item, CrudOperations operation) where T : SObject;
        SalesforceResponse PerformCrudOperation<T>(T[] items, CrudOperations operation) where T : SObject;
        JobInfo CreateBulkJob<T>(JobConfig config);
        BatchInfo AddBatch<T>(T[] items, string jobId);
        SalesforceResponse CloseBulkJob(string jobId);
    }
}