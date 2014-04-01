using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using FastMember;
using SalesforceMagic.Entities;
using SalesforceMagic.Extensions;

namespace SalesforceMagic.ORM
{
    internal static class ResponseReader
    {
        internal static T ReadSimpleResponse<T>(XmlDocument document)
        {
            return ReadSimpleResponse<T>(GetResultNode(document));
        }

        internal static T ReadSimpleResponse<T>(XmlNode node)
        {
            Type type = typeof (T);
            bool ns = type.BaseType ==  typeof (SObject);
            T obj = Activator.CreateInstance<T>();
            TypeAccessor accessor = TypeAccessor.Create(type);

            foreach (PropertyInfo property in type.GetProperties())
            {
                string name = ns ? "sf:" + property.GetName() : property.GetName();
                string value = node.GetValue(name);
                if (value != null) accessor[obj, property.Name] = value;
            }

            return obj;
        }

        internal static IEnumerable<T> ReadArrayResponse<T>(XmlDocument document)
        {
            return (from XmlNode node in GetRecordNodes(document) select ReadSimpleResponse<T>(node)).ToList();
        }

        private static XmlNode GetResultNode(XmlDocument document)
        {
            return document.GetElementsByTagName("result")[0];
        }

        private static XmlNodeList GetRecordNodes(XmlDocument document)
        {
            return document.GetElementsByTagName("records");
        }
    }
}