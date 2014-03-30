using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SalesforceMagic.Attributes;

namespace SalesforceMagic.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<string> GetPropertyNames(this Type type)
        {
            return type.GetProperties().GetNames();
        }

        public static IEnumerable<string> GetNames(this PropertyInfo[] infos)
        {
            return infos.Select(x => x.GetName());
        }

        public static string GetName(this PropertyInfo info)
        {
            return info.GetCustomAttribute<SalesforceNameAttribute>() != null
                ? info.GetCustomAttribute<SalesforceNameAttribute>().Name
                : info.Name;
        }

        public static string GetName(this Type type)
        {
            return type.GetCustomAttribute<SalesforceNameAttribute>().Name;
        }

        public static string GetLambdaString(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.AndAlso:
                    return GetLambdaString(((BinaryExpression) expression).Left) + " AND " +
                           GetLambdaString(((BinaryExpression) expression).Right);
                case ExpressionType.Equal:
                    return GetLambdaString(((BinaryExpression)expression).Left) + " = " + ((BinaryExpression)expression).Right;
                case ExpressionType.Lambda:
                    return GetLambdaString(((LambdaExpression)expression).Body);
                case ExpressionType.MemberAccess:
                    PropertyInfo propertyInfo = ((MemberExpression)expression).Member as PropertyInfo;
                    return propertyInfo.GetName();
                case ExpressionType.Constant:
                    return ((ConstantExpression) expression).Value.ToString();
                default:
                    return null;
            }
        }
    }
}