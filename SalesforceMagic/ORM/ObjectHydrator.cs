using System;
using System.Collections.Generic;
using FastMember;

namespace SalesforceMagic.ORM
{
    public static class ObjectHydrator
    {
        internal static IDictionary<Type, TypeAccessor> CachedAccessors = new Dictionary<Type, TypeAccessor>();

        public static TypeAccessor GetAccessor(Type type)
        {
            if (CachedAccessors.ContainsKey(type)) return CachedAccessors[type];
            TypeAccessor accessor = TypeAccessor.Create(type);
            CachedAccessors.Add(type, accessor);

            return accessor;
        }
    }
}