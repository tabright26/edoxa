﻿// Filename: SpecificationFactory.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Specifications.Abstractions;

namespace eDoxa.Seedwork.Domain.Specifications
{
    public sealed class SpecificationFactory
    {
        private static readonly Lazy<SpecificationFactory> Lazy = new Lazy<SpecificationFactory>(() => new SpecificationFactory());

        public static SpecificationFactory Instance => Lazy.Value;

        public ISpecification<TEntity> Create<TEntity>()
        where TEntity : IEntity, IAggregateRoot
        {
            return new NoneSpecification<TEntity>();
        }
    }
}
