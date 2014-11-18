using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using FastMember;
using SalesforceMagic.Entities;
using SalesforceMagic.Extensions;
using SalesforceMagic.ORM.BaseRequestTemplates;
using SalesforceMagic.SoapApi.Responses;

namespace SalesforceMagic.ORM
{
    internal static class ResponseReader
    {
        internal static SalesforceResponse ReadSimpleResponse(XmlDocument document)
        {
            SalesforceResponse response = new SalesforceResponse();
            XmlNodeList results = GetNamedNodes(document, "result");

            if (results.Count > 1)
            {
                foreach (XmlNode node in results)
                {
                    RecordResult result = ReadSimpleResponse<RecordResult>(node, document);
                    response.Results.Add(result);

                    if (!result.Success)
                    {
                        response.Errors.Add(result.Message);
                    }
                }
            }
            else
            {
                response.Result = ReadSimpleResponse<RecordResult>(results[0], document);
            }

            return response;
        }

        internal static T ReadGenericResponse<T>(XmlDocument document, string rootName)
        {
            return ReadSimpleResponse<T>(GetNamedNodes(document, rootName)[0], document);
        }

        internal static T ReadSimpleResponse<T>(XmlNode node, XmlDocument document)
        {
            Type type = typeof (T);
            bool ns = type.BaseType ==  typeof (SObject);
            T obj = Activator.CreateInstance<T>();
            TypeAccessor accessor = ObjectHydrator.GetAccessor(type);

            foreach (PropertyInfo property in type.GetProperties())
            {
                string value;
                Type propertyType = property.PropertyType;
                string name = ns ? "sf:" + property.GetName() : property.GetName();

                if (name.Contains("__r"))
                {
                    string referencedProperty = name.Substring(name.LastIndexOf("__r", StringComparison.Ordinal) + 4);
                    referencedProperty = ns ? "sf:" + referencedProperty : referencedProperty;
                    name = name.Substring(0, name.LastIndexOf("__r", StringComparison.Ordinal) + 3);
                    XmlNode referencedNode = FindChildNode(name, node);
                    if (referencedNode == null) continue;

                    value = referencedNode.GetValue(referencedProperty);
                }
                else value = node.GetValue(name);

                if (value == null)
                {
                    IEnumerable<XElement> nodes = GetNamedNodes(node, name);
                    if (nodes == null || !nodes.Any()) continue;
                    XElement child = nodes.FirstOrDefault();
                    if (child == null) continue;

                    value = child.Value;
                };

                if (propertyType == typeof(string)) accessor[obj, property.Name] = value;
                if (propertyType == typeof(bool)) accessor[obj, property.Name] = Convert.ToBoolean(value);
                if (propertyType == typeof(int)) accessor[obj, property.Name] = Convert.ToInt32(value);
                if (propertyType == typeof(double)) accessor[obj, property.Name] = Convert.ToDouble(value);
                if (propertyType == typeof(decimal)) accessor[obj, property.Name] = Convert.ToDecimal(value);
                if (propertyType == typeof(DateTime)) accessor[obj, property.Name] = Convert.ToDateTime(value);
            }

            return obj;
        }

        internal static T[] ReadArrayResponse<T>(XmlDocument document)
        {
            return (from XmlNode node in GetNamedNodes(document, "records") select ReadSimpleResponse<T>(node, document)).ToArray();
        }

        private static XmlNodeList GetNamedNodes(XmlDocument document, string name)
        {
            return document.GetElementsByTagName(name);
        }

        private static XElement[] GetNamedNodes(XmlNode node, string name)
        {
            XDocument document = XDocument.Parse(node.OuterXml);
            return document.Descendants().Where(x => x.Name.LocalName == name).ToArray();
        }

        private static XmlNode FindChildNode(string name, XmlNode node)
        {
            return node.ChildNodes.Cast<XmlNode>().Where(x => x.Name == name).ToArray().FirstOrDefault();
        }
    }
}