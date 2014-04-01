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
            return infos.Select(x => x.GetName());
        }

        internal static string GetName(this PropertyInfo info)
        {
            return info.GetCustomAttribute<SalesforceNameAttribute>() != null
                ? info.GetCustomAttribute<SalesforceNameAttribute>().Name
                : info.Name;
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