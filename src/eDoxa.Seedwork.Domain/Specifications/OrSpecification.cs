﻿// Filename: OrSpecification.cs
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
    internal sealed class OrSpecification<TEntity> : Specification<TEntity>
    where TEntity : IAggregateRoot
    {
        private readonly ISpecification<TEntity> _left;
        private readonly ISpecification<TEntity> _right;

        public OrSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
        {
            _left = left;
            _right = right;
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            return _left.IsSatisfiedBy(entity) || _right.IsSatisfiedBy(entity);
        }
    }
}