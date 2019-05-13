﻿// Filename: RoleConfiguration.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Identity.Infrastructure.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure([NotNull] EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(IdentityDbContext.Roles));

            builder.HasMany(user => user.Claims)
                .WithOne()
                .HasForeignKey(role => role.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //builder.Metadata.FindNavigation(nameof(Role.Claims)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}