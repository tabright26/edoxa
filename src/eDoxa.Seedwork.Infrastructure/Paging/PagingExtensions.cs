// Filename: PagingExtensions.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDoxa.Seedwork.Infrastructure.Paging
{
    public static class PagingExtensions
    {
        public static Task<PagedList<TSource>> ToPagedListAsync<TSource>(
            this IQueryable<TSource> source,
            int pageIndex = 0,
            int pageSize = 10,
            int? totalCount = null)
        {
            return Task.FromResult(new PagedList<TSource>(source, pageIndex, pageSize, totalCount));
        }

        public static Task<PagedList<TSource>> ToPagedListAsync<TSource>(
            this IEnumerable<TSource> source,
            int pageIndex = 0,
            int pageSize = 10,
            int? totalCount = null)
        {
            return Task.FromResult(new PagedList<TSource>(source, pageIndex, pageSize, totalCount));
        }

        public static PagedList<TSource> ToPagedList<TSource>(this IQueryable<TSource> source, int pageIndex = 0, int pageSize = 10, int? totalCount = null)
        {
            return new PagedList<TSource>(source, pageIndex, pageSize, totalCount);
        }

        public static PagedList<TSource> ToPagedList<TSource>(this IEnumerable<TSource> source, int pageIndex = 0, int pageSize = 10, int? totalCount = null)
        {
            return new PagedList<TSource>(source, pageIndex, pageSize, totalCount);
        }
    }
}