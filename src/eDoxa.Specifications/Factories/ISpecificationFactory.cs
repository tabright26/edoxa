// Filename: ISpecificationFactory.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Specifications.Factories
{
    public interface ISpecificationFactory
    {
        ISpecification<TEntity> Create<TEntity>()
        where TEntity : IEntity, IAggregateRoot;
    }
}