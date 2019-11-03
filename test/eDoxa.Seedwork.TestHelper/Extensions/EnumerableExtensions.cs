// Filename: EnumerableExtensions.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.TestHelper.Mocks;

namespace eDoxa.Seedwork.TestHelper.Extensions
{
    public static class EnumerableExtensions
    {
        public static MockAsyncEnumerable<T> ToMockAsyncEnumerable<T>(this IEnumerable<T> enumerable)
        {
            return new MockAsyncEnumerable<T>(enumerable);
        }
    }
}
