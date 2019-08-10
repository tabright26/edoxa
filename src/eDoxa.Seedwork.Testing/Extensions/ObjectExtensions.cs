// Filename: ObjectExtensions.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Collections.ObjectModel;

using eDoxa.Seedwork.Testing.Mocks;

namespace eDoxa.Seedwork.Testing.Extensions
{
    public static class ObjectExtensions
    {
        public static List<T> ToList<T>(this T obj)
        where T : class
        {
            return new List<T>
            {
                obj
            };
        }

        public static T[] ToArray<T>(this T obj)
        where T : class
        {
            return new[] {obj};
        }

        public static HashSet<T> ToHashSet<T>(this T obj)
        where T : class
        {
            return new HashSet<T>
            {
                obj
            };
        }

        public static Collection<T> ToCollection<T>(this T obj)
        where T : class
        {
            return new Collection<T>
            {
                obj
            };
        }

        public static MockAsyncEnumerable<T> ToMockAsyncEnumerable<T>(this T obj)
        where T : class
        {
            return new MockAsyncEnumerable<T>(obj.ToArray());
        }

    }
}
