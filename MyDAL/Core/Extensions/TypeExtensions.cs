﻿using System;

namespace HPC.DAL.Core.Extensions
{
    internal static class TypeExtensions
    {
        internal static bool IsList(this Type type)
        {
            if (type.IsGenericType
                && type.GetGenericTypeDefinition() == XConfig.CSTC.ListT)
            {
                return true;
            }
            return false;
        }

        internal static bool IsNullable(this Type type)
        {
            if (type.IsGenericType
                && type.GetGenericTypeDefinition() == XConfig.CSTC.NullableT)
            {
                return true;
            }
            return false;
        }

        internal static bool IsSingleColumn(this Type type)
        {
            if (type.IsValueType
                || type == XConfig.CSTC.String)
            {
                return true;
            }
            return false;
        }
    }
}
