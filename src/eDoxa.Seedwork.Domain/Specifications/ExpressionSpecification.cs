// Filename: ExpressionSpecification.cs
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

namespace eDoxa.Seedwork.Domain.Specifications
{
    public sealed class ExpressionSpecification<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> _predicate;

        public ExpressionSpecification(Expression<Func<T, bool>> predicate)
        {
            _predicate = predicate;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return _predicate;
        }
    }
}
