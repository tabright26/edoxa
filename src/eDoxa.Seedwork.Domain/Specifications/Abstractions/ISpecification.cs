// Filename: ISpecification.cs
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

namespace eDoxa.Seedwork.Domain.Specifications.Abstractions
{
    public interface ISpecification<TEntity>
    where TEntity : IEntity, IAggregateRoot
    {
        Expression<Func<TEntity, bool>> ToExpression();

        bool IsSatisfiedBy(TEntity entity);

        ISpecification<TEntity> And(ISpecification<TEntity> specification);

        ISpecification<TEntity> And(Expression<Func<TEntity, bool>> right);

        ISpecification<TEntity> Or(ISpecification<TEntity> specification);

        ISpecification<TEntity> Or(Expression<Func<TEntity, bool>> right);

        ISpecification<TEntity> Not();
    }
}