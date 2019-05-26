// Filename: StaticHelper.cs
// Date Created: 2019-05-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace eDoxa.Reflection
{
    public static class StaticUtils
    {
        public static IEnumerable<T> GetDeclaredFields<T>()
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(field => field.GetValue(null))
                .Where(typeObject => typeObject is T)
                .Cast<T>();
        }
    }
}
