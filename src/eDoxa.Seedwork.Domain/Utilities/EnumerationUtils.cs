// Filename: EnumerationUtils.cs
// Date Created: 2019-05-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Seedwork.Domain.Utilities
{
    public static class EnumerationUtils
    {
        private static readonly Type[] Enumerations;

        static EnumerationUtils()
        {
            Enumerations = GetSuperclassTypes();
        }

        //public static IEnumerable<IEnumeration> GetAll(object type)
        //{
        //    foreach (var fieldInfo in type.GetType().GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly))
        //    {
        //        if (fieldInfo.GetValue(null) is IEnumeration enumeration)
        //        {
        //            yield return enumeration;
        //        }
        //    }
        //}

        public static bool IsDefined(object obj)
        {
            return Enumerations.Any(type => type == obj.GetType());
        }

        private static Type[] GetSuperclassTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && type.GetGenericTypeDefinition() == typeof(Enumeration<>))
                .ToArray();
        }
    }
}