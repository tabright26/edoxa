// Filename: ReflectionUtilities.cs
// Date Created: 2019-05-29
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

namespace eDoxa.Seedwork.Domain.Reflection
{
    public static class ReflectionUtilities
    {
        public static IEnumerable<T> GetDeclaredOnlyFields<T>()
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(fieldInfo => fieldInfo.GetValue(null))
                .Where(obj => obj is T)
                .Cast<T>();
        }
    }
}
