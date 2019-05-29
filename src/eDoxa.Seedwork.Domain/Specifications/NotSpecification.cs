// Filename: NotSpecification.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Linq.Expressions;

using eDoxa.Seedwork.Domain.Specifications.Abstractions;

namespace eDoxa.Seedwork.Domain.Specifications
{
    public sealed class NotSpecification<TEntity> : Specification<TEntity>
    where TEntity : IEntity, IAggregateRoot
    {
        private readonly Expression<Func<TEntity, bool>> _left;

        public NotSpecification(ISpecification<TEntity> left)
        {
            _left = left.ToExpression();
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var notExpression = Expression.Not(_left.Body);

            return Expression.Lambda<Func<TEntity, bool>>(notExpression, _left.Parameters.Single());
        }
    }
}