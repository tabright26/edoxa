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

using eDoxa.Seedwork.Domain;

namespace eDoxa.Specifications
{
    public abstract class Specification<TEntity> : ISpecification<TEntity>
    where TEntity : IEntity, IAggregateRoot
    {
        public abstract Expression<Func<TEntity, bool>> ToExpression();

        public bool IsSatisfiedBy(TEntity entity)
        {
            return this.ToExpression().Compile()(entity);
        }

        public ISpecification<TEntity> And(ISpecification<TEntity> other)
        {
            return new AndSpecification<TEntity>(this, other);
        }

        public ISpecification<TEntity> And(Expression<Func<TEntity, bool>> other)
        {
            return new AndSpecification<TEntity>(this, new ExpressionSpecification<TEntity>(other));
        }

        public ISpecification<TEntity> Or(ISpecification<TEntity> other)
        {
            return new OrSpecification<TEntity>(this, other);
        }

        public ISpecification<TEntity> Or(Expression<Func<TEntity, bool>> other)
        {
            return new OrSpecification<TEntity>(this, new ExpressionSpecification<TEntity>(other));
        }

        public ISpecification<TEntity> Not()
        {
            return new NotSpecification<TEntity>(this);
        }
    }
}