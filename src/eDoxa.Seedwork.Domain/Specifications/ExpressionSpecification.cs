// Filename: ExpressionSpecification.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq.Expressions;

namespace eDoxa.Seedwork.Domain.Specifications
{
    public sealed class ExpressionSpecification<TEntity> : Specification<TEntity>
    where TEntity : IEntity, IAggregateRoot
    {
        private readonly Expression<Func<TEntity, bool>> _predicate;

        public ExpressionSpecification(Expression<Func<TEntity, bool>> predicate)
        {
            _predicate = predicate;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return _predicate;
        }
    }
}