// Filename: SpecificationFactory.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Domain.Specifications.Factories
{
    public sealed class SpecificationFactory
    {
        private static readonly Lazy<SpecificationFactory> Lazy = new Lazy<SpecificationFactory>(() => new SpecificationFactory());

        public static SpecificationFactory Instance => Lazy.Value;

        public ISpecification<TEntity> CreateSpecification<TEntity>()
        where TEntity : IEntity, IAggregateRoot
        {
            return new NoneSpecification<TEntity>();
        }
    }
}