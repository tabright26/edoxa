// Filename: UserConfiguration.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Identity.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure([NotNull] EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(IdentityDbContext.Users));

            builder.HasMany(user => user.Roles).WithOne().HasForeignKey(role => role.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Claims).WithOne().HasForeignKey(role => role.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Logins).WithOne().HasForeignKey(role => role.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Tokens).WithOne().HasForeignKey(role => role.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            //builder.Metadata.FindNavigation(nameof(User.Roles)).SetPropertyAccessMode(PropertyAccessMode.Field);

            //builder.Metadata.FindNavigation(nameof(User.Claims)).SetPropertyAccessMode(PropertyAccessMode.Field);

            //builder.Metadata.FindNavigation(nameof(User.Logins)).SetPropertyAccessMode(PropertyAccessMode.Field);

            //builder.Metadata.FindNavigation(nameof(User.Tokens)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
