using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using SalesforceMagic.Attributes;

namespace SalesforceMagic.Extensions
{
    internal static class TypeExtensions
    {
        internal static IEnumerable<string> GetPropertyNames(this Type type)
        {
            return type.GetProperties().GetNames();
        }

        internal static IEnumerable<string> GetNames(this PropertyInfo[] infos)
        {
            // TODO: There has to be a better way to do this, the Id field needs to be first.
            List<string> names = new List<string> { GetName(infos.FirstOrDefault(x => x.Name == "Id")) };
            names.AddRange(infos.Where(x => x.Name != "Id").Select(x => x.GetName()));

            return names;
        }

        internal static string GetName(this PropertyInfo info)
        {
            return info.GetCustomAttribute<SalesforceNameAttribute>() != null
                ? info.GetCustomAttribute<SalesforceNameAttribute>().Name
                : info.Name;
        }

        internal static string GetCsvHeaders<T>(this IEnumerable<T> items)
        {
            Type type = typeof(T);
            string[] values = type.GetProperties().Select(x => x.GetName()).ToArray();

            return String.Join(",", values);
        }

        internal static string GetName(this Type type)
        {
            return type.GetCustomAttribute<SalesforceNameAttribute>().Name;
        }

        internal static string GetValue(this XmlNode node, string name)
        {
            var result = node[name];
            return result != null ? result.InnerText : null;
        }
    }
}