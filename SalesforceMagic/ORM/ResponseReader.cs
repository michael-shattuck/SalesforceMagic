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
        internal static IDictionary<Type, TypeAccessor> CachedAccessors = new Dictionary<Type, TypeAccessor>();

        internal static T ReadSimpleResponse<T>(XmlDocument document)
        {
            return ReadSimpleResponse<T>(GetResultNode(document), document);
        }

        internal static T ReadSimpleResponse<T>(XmlNode node, XmlDocument document)
        {
            Type type = typeof (T);
            bool ns = type.BaseType ==  typeof (SObject);
            T obj = Activator.CreateInstance<T>();
            TypeAccessor accessor = GetTypeAccessor(type);

            foreach (PropertyInfo property in type.GetProperties())
            {
                string name = ns ? "sf:" + property.GetName() : property.GetName();
                string value = node.GetValue(name);
                if (value != null) accessor[obj, property.Name] = value;
            }

            return obj;
        }

        private static TypeAccessor GetTypeAccessor(Type type)
        {
            if (CachedAccessors.ContainsKey(type)) return CachedAccessors[type];
            TypeAccessor accessor = TypeAccessor.Create(type);
            CachedAccessors.Add(type, accessor);

            return accessor;
        }

        internal static IEnumerable<T> ReadArrayResponse<T>(XmlDocument document)
        {
            return (from XmlNode node in GetRecordNodes(document) select ReadSimpleResponse<T>(node, document)).ToList();
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