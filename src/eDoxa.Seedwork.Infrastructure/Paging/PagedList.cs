// Filename: PagedList.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Seedwork.Infrastructure.Paging
{
    /// <summary>
    ///     https://github.com/martijnboland/MvcPaging/blob/master/src/MvcPaging/PagedList.cs
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public class PagedList<TSource> : List<TSource>, IPagedList<TSource>
    {
        public PagedList(IEnumerable<TSource> source, int index, int pageSize, int? totalCount = null) : this(source.AsQueryable(), index, pageSize, totalCount)
        {
        }

        public PagedList(IQueryable<TSource> source, int index, int pageSize, int? totalCount = null)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Value can not be below 0.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Value can not be less than 1.");
            }

            if (source == null)
            {
                source = new List<TSource>().AsQueryable();
            }

            var realTotalCount = source.Count();

            PageSize = pageSize;
            PageIndex = index;
            TotalItemCount = totalCount ?? realTotalCount;
            PageCount = TotalItemCount > 0 ? (int) Math.Ceiling(TotalItemCount / (double) PageSize) : 0;

            HasPreviousPage = PageIndex > 0;
            HasNextPage = PageIndex < PageCount - 1;
            IsFirstPage = PageIndex <= 0;
            IsLastPage = PageIndex >= PageCount - 1;

            ItemStart = (PageIndex * PageSize) + 1;
            ItemEnd = Math.Min((PageIndex * PageSize) + PageSize, TotalItemCount);

            if (TotalItemCount <= 0)
            {
                return;
            }

            this.AddRange(realTotalCount != TotalItemCount ? source.Take(PageSize) : source.Skip(PageIndex * PageSize).Take(PageSize));
        }

        public int PageCount { get; private set; }
        public int TotalItemCount { get; private set; }
        public int PageIndex { get; private set; }

        public int PageNumber
        {
            get
            {
                return PageIndex + 1;
            }
        }

        public int PageSize { get; private set; }
        public bool HasPreviousPage { get; private set; }
        public bool HasNextPage { get; private set; }
        public bool IsFirstPage { get; private set; }
        public bool IsLastPage { get; private set; }
        public int ItemStart { get; private set; }
        public int ItemEnd { get; private set; }
    }
}