using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SalesforceMagic.Configuration;
using SalesforceMagic.Entities;

namespace SalesforceMagic.Abstract
{
    public interface ISalesforceClient : IDisposable
    {
        void ChangeEnvironment(SalesforceConfig config, bool login = false);

        string GetSessionId();

        SalesforceSession Login();

        T[] Query<T>(Expression<Func<T, bool>> predicate, int limit = 0) where T : SObject;

        SalesforceResponse Insert<T>(T[] items);

        SalesforceResponse Insert<T>(T item);

        SalesforceResponse Upsert<T>(T[] items);

        SalesforceResponse Upsert<T>(T item);

        SalesforceResponse Update<T>(T[] items);

        SalesforceResponse Update<T>(T item);

        SalesforceResponse Delete<T>(T[] items);

        SalesforceResponse Delete<T>(T item);

        SalesforceResponse Delete<T>(string id);

        SalesforceResponse Delete<T>(Expression<Func<T, bool>> predicate, int limit = 0) where T : SObject;
    }
}