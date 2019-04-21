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

namespace eDoxa.Seedwork.Domain.Specifications
{
    public abstract class ExpressionSpecification<TEntity> : Specification<TEntity>
    where TEntity : IEntity, IAggregateRoot
    {
        private readonly Func<TEntity, bool> _expression;

        protected ExpressionSpecification(Func<TEntity, bool> expression)
        {
            _expression = expression;
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            return _expression(entity);
        }
    }
}