// Filename: NoneSpecification.cs
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
    public sealed class NoneSpecification<TEntity> : Specification<TEntity>
    where TEntity : IEntity, IAggregateRoot
    {
        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return _ => true;
        }
    }
}