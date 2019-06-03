// Filename: CollectionOwnershipBuilderExtensions.cs
// Date Created: 2019-06-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class CollectionOwnershipBuilderExtensions
    {
        private const string Id = nameof(Id);

        public static void HasShadowForeignKey<TEntityId>(this CollectionOwnershipBuilder builder, string propertyName)
        where TEntityId : EntityId<TEntityId>, new()
        {
            builder.HasForeignKey(propertyName);

            builder.Property<TEntityId>(propertyName)
                .HasConversion(entityId => entityId.ToGuid(), value => EntityId<TEntityId>.FromGuid(value))
                .HasColumnName(propertyName)
                .IsRequired();
        }

        public static void HasShadowKey(this CollectionOwnershipBuilder builder, params string[] propertyNames)
        {
            propertyNames.Append(Id);

            builder.Property<Guid>(Id).ValueGeneratedOnAdd().IsRequired();

            builder.HasKey(propertyNames);
        }
    }
}
