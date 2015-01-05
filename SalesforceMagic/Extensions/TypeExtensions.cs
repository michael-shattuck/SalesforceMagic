using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using SalesforceMagic.Attributes;
using SalesforceMagic.Exceptions;

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
            SalesforceNameAttribute attribute = type.GetCustomAttribute<SalesforceNameAttribute>();
            return attribute != null
                ? attribute.Name
                : type.Name;
        }

        internal static string GetValue(this XmlNode node, string name)
        {
            var result = node[name];
            return result != null ? result.InnerText : null;
        }

        internal static bool ToBoolean(this string value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (FormatException e) { throw new SalesforceRequestException(e.Message); }
        }

        internal static int ToInt(this string value)
        {
            try
            {
                return value.Contains(".")
                    ? (int) Convert.ToDecimal(value)
                    : Convert.ToInt32(value);
            }
            catch (FormatException e) { throw new SalesforceRequestException(e.Message); }
        }

        internal static double ToDouble(this string value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch (FormatException e) { throw new SalesforceRequestException(e.Message); }
        }

        internal static decimal ToDecimal(this string value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch (FormatException e) { throw new SalesforceRequestException(e.Message); }
        }

        internal static float ToFloat(this string value)
        {
            try
            {
                return value.Contains(".")
                    ? (int)Convert.ToDecimal(value)
                    : Convert.ToInt32(value);
            }
            catch (FormatException e) { throw new SalesforceRequestException(e.Message); }
        }

        internal static DateTime ToDateTime(this string value)
        {
            try
            {
                return string.IsNullOrEmpty(value)
                    ? DateTime.MinValue
                    : Convert.ToDateTime(value);
            }
            catch (FormatException e) { throw new SalesforceRequestException(e.Message); }
        }



        internal static bool? ToNullableBoolean(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (FormatException e) { throw new SalesforceRequestException(e.Message); }
        }

        internal static int? ToNullableInt(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            try
            {
                return value.Contains(".")
                    ? (int)Convert.ToDecimal(value)
                    : Convert.ToInt32(value);
            }
            catch (FormatException e) { throw new SalesforceRequestException(e.Message); }
        }

        internal static double? ToNullableDouble(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            try
            {
                return Convert.ToDouble(value);
            }
            catch (FormatException e) { throw new SalesforceRequestException(e.Message); }
        }

        internal static decimal? ToNullableDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            try
            {
                return Convert.ToDecimal(value);
            }
            catch (FormatException e) { throw new SalesforceRequestException(e.Message); }
        }

        internal static float? ToNullableFloat(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            try
            {
                return value.Contains(".")
                    ? (int)Convert.ToDecimal(value)
                    : Convert.ToInt32(value);
            }
            catch (FormatException e) { throw new SalesforceRequestException(e.Message); }
        }

        internal static DateTime? ToNullableDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            try
            {
                return string.IsNullOrEmpty(value)
                    ? DateTime.MinValue
                    : Convert.ToDateTime(value);
            }
            catch (FormatException e) { throw new SalesforceRequestException(e.Message); }
        }
    }
}