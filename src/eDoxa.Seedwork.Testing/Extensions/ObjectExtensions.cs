// Filename: ObjectExtensions.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

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

        public static MockAsyncEnumerable<T> ToMockAsyncEnumerable<T>(this T obj)
        where T : class
        {
            return new MockAsyncEnumerable<T>(obj.ToList());
        }
    }
}
