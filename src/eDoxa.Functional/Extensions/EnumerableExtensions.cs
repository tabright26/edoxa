// Filename: EnumerableExtensions.cs
// Date Created: 2019-04-21
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

using eDoxa.Functional.Option;

namespace eDoxa.Functional.Extensions
{
    public static class EnumerableExtensions
    {
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> sequence)
        {
            return sequence.Select(selector => (Option<T>) new Some<T>(selector))
                .DefaultIfEmpty(None.Value)
                .First();
        }

        public static Option<T> FirstOrNone<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            return sequence.Where(predicate).FirstOrNone();
        }

        public static IEnumerable<TResult> OptionalSelect<T, TResult>(this IEnumerable<T> sequence, Func<T, Option<TResult>> map)
        {
            return sequence.Select(map)
                .OfType<Some<TResult>>()
                .Select(some => some.Content);
        }

        public static IEnumerable<T> OptionalOrderBy<T, TResult>(this IEnumerable<T> sequence, Func<T, Option<TResult>> map)
        {
            return sequence.OrderBy(keySelector => map(keySelector).Default());
        }

        public static IEnumerable<T> OptionalOrderByDescending<T, TResult>(this IEnumerable<T> sequence, Func<T, Option<TResult>> map)
        {
            return sequence.OrderByDescending(keySelector => map(keySelector).Default());
        }
    }
}