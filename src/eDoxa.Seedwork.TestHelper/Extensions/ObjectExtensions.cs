// Filename: ObjectExtensions.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.TestHelper.Mocks;

namespace eDoxa.Seedwork.TestHelper.Extensions
{
    public static class ObjectExtensions
    {
        public static MockAsyncEnumerable<T> ToMockAsyncEnumerable<T>(this T obj)
        where T : class
        {
            return new MockAsyncEnumerable<T>(new List<T>
            {
                obj
            });
        }
    }
}
