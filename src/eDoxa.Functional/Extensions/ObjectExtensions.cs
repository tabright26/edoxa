// Filename: ObjectExtensions.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Functional.Option;

namespace eDoxa.Functional.Extensions
{
    public static class ObjectExtensions
    {
        public static Option<T> When<T>(this T obj, bool condition)
        {
            return condition ? (Option<T>) new Some<T>(obj) : None.Value;
        }

        public static Option<T> When<T>(this T obj, Func<T, bool> predicate)
        {
            return obj.When(predicate(obj));
        }

        public static Option<T> NoneIfNull<T>(this T obj)
        {
            return obj.When(!ReferenceEquals(obj, null));
        }
    }
}