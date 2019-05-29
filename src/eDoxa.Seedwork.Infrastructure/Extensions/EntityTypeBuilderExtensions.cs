// Filename: EntityTypeBuilderExtensions.cs
// Date Created: 2019-05-26
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
using eDoxa.Seedwork.Domain.Aggregate;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static PropertyBuilder<TEntityId> EntityId<TEntity, TEntityId>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TEntityId>> expression
        )
        where TEntity : class, IEntity
        where TEntityId : EntityId<TEntityId>, new()
        {
            return builder.Property(expression).HasConversion(entityId => entityId.ToGuid(), entityId => EntityId<TEntityId>.FromGuid(entityId));
        }

        public static PropertyBuilder<TEnumeration> Enumeration<TEntity, TEnumeration>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TEnumeration>> expression
        )
        where TEntity : class, IEntity
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            return builder.Property(expression).HasConversion(entityId => entityId.Value, entityId => Enumeration<TEnumeration>.FromValue(entityId));
        }
    }
}
