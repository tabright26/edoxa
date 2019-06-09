// Filename: AndSpecification.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq.Expressions;

using eDoxa.Seedwork.Domain.Specifications.Abstractions;

namespace eDoxa.Seedwork.Domain.Specifications
{
    public sealed class AndSpecification<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> _left;
        private readonly Expression<Func<T, bool>> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left.ToExpression();
            _right = right.ToExpression();
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var visitor = new ExpressionSpecificationVisitor(_left.Parameters[0], _right.Parameters[0]);

            var binaryExpression = Expression.AndAlso(visitor.Visit(_left.Body), _right.Body);

            return Expression.Lambda<Func<T, bool>>(binaryExpression, _right.Parameters);
        }
    }
}
