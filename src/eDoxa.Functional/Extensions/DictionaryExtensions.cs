// Filename: DictionaryExtensions.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Functional.Option;

namespace eDoxa.Functional.Extensions
{
    public static class DictionaryExtensions
    {
        public static Option<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value)
                ? (Option<TValue>) new Some<TValue>(value)
                : None.Value;
        }
    }
}