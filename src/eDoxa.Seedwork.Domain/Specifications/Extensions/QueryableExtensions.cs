// Filename: QueryableExtensions.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Specifications.Abstractions;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Specifications.Extensions
{
    public static class QueryableExtensions
    {
        [CanBeNull]
        public static TEntity SingleOrDefault<TEntity>(this IQueryable<TEntity> queryable, ISpecification<TEntity> specification)
        where TEntity : IEntity, IAggregateRoot
        {
            return queryable.Where(specification).SingleOrDefault();
        }

        public static IList<TEntity> ToList<TEntity>(this IQueryable<TEntity> queryable, ISpecification<TEntity> specification)
        where TEntity : IEntity, IAggregateRoot
        {
            return queryable.Where(specification).ToList();
        }

        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> queryable, ISpecification<TEntity> specification)
        where TEntity : IEntity, IAggregateRoot
        {
            return queryable.Where(specification.ToExpression());
        }
    }
}