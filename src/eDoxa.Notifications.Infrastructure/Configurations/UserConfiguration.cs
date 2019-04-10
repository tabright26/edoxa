// Filename: UserConfiguration.cs
// Date Created: 2019-03-31
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Notifications.Infrastructure.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(NotificationsDbContext.Users));

            builder.Property(user => user.Id)
                   .HasConversion(userId => userId.ToGuid(), value => UserId.FromGuid(value))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(user => user.Notifications)
                   .WithOne(notification => notification.User)
                   .HasForeignKey(nameof(UserId))
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Metadata.FindNavigation(nameof(User.Notifications)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(user => user.Id);
        }
    }
}