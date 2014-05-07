using System;
using System.Collections.Generic;
using FastMember;

namespace SalesforceMagic.ORM
{
    public static class ObjectHydrator
    {
        internal static IDictionary<string, TypeAccessor> CachedAccessors = new Dictionary<string, TypeAccessor>();

        public static TypeAccessor GetAccessor(Type type)
        {
            string name = type.Name;
            if (CachedAccessors.ContainsKey(name)) return CachedAccessors[name];
            TypeAccessor accessor = TypeAccessor.Create(type);
            CachedAccessors.Add(name, accessor);

            return accessor;
        }
    }
}