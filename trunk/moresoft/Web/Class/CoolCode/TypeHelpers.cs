﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolCode.Web
{
    internal static class TypeHelpers
    {

        public static bool TypeAllowsNullValue(Type type)
        {
            // reference types allow null values
            if (!type.IsValueType)
            {
                return true;
            }

            // nullable value types allow null values
            // code lifted from System.Nullable.GetUnderlyingType()
            if (type.IsGenericType && !type.IsGenericTypeDefinition && (type.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                return true;
            }

            // no other types allow null values
            return false;
        }

    }
}
