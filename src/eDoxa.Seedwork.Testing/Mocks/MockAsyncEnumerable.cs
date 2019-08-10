// Filename: TestAsyncEnumerable.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace eDoxa.Seedwork.Testing.Mocks
{
    public class MockAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        internal MockAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
        {
        }

        internal MockAsyncEnumerable(Expression expression) : base(expression)
        {
        }

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new MockAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider => new MockAsyncQueryProvider<T>(this);
    }
}
