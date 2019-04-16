// Filename: NotSpecification.cs
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
    internal sealed class NotSpecification<TEntity> : Specification<TEntity>
    where TEntity : IAggregateRoot
    {
        private readonly ISpecification<TEntity> _other;

        public NotSpecification(ISpecification<TEntity> other)
        {
            _other = other;
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            return !_other.IsSatisfiedBy(entity);
        }
    }
}