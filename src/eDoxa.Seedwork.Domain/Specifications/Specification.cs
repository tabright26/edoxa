// Filename: Specification.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Seedwork.Domain.Specifications
{
    public abstract class Specification<TEntity> : ISpecification<TEntity>
    where TEntity : IAggregateRoot
    {
        public abstract bool IsSatisfiedBy(TEntity entity);

        public Specification<TEntity> And(Specification<TEntity> other)
        {
            return new AndSpecification<TEntity>(this, other);
        }

        public Specification<TEntity> Or(Specification<TEntity> other)
        {
            return new OrSpecification<TEntity>(this, other);
        }

        public Specification<TEntity> Not()
        {
            return new NotSpecification<TEntity>(this);
        }
    }
}