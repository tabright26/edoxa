// Filename: Specification.cs
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

using eDoxa.Seedwork.Domain.Specifications.Abstractions;

namespace eDoxa.Seedwork.Domain.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity)
        {
            return this.ToExpression().Compile()(entity);
        }

        public ISpecification<T> And(ISpecification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        public ISpecification<T> And(Expression<Func<T, bool>> other)
        {
            return new AndSpecification<T>(this, new ExpressionSpecification<T>(other));
        }

        public ISpecification<T> Or(ISpecification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

        public ISpecification<T> Or(Expression<Func<T, bool>> other)
        {
            return new OrSpecification<T>(this, new ExpressionSpecification<T>(other));
        }

        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }
}