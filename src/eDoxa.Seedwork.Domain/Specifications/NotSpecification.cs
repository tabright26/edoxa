// Filename: NotSpecification.cs
// Date Created: 2019-06-01
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
    public sealed class NotSpecification<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> _left;

        public NotSpecification(ISpecification<T> left)
        {
            _left = left.ToExpression();
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var notExpression = Expression.Not(_left.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, _left.Parameters.Single());
        }
    }
}
