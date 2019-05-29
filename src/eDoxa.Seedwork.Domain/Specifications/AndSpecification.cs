// Filename: AndSpecification.cs
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
    public sealed class AndSpecification<TEntity> : Specification<TEntity>
    where TEntity : IEntity, IAggregateRoot
    {
        private readonly Expression<Func<TEntity, bool>> _left;
        private readonly Expression<Func<TEntity, bool>> _right;

        public AndSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
        {
            _left = left.ToExpression();
            _right = right.ToExpression();
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var visitor = new ExpressionSpecificationVisitor(_left.Parameters[0], _right.Parameters[0]);

            var binaryExpression = Expression.AndAlso(visitor.Visit(_left.Body), _right.Body);

            return Expression.Lambda<Func<TEntity, bool>>(binaryExpression, _right.Parameters);
        }
    }
}