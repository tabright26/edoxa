// Filename: NotificationConfiguration.cs
// Date Created: 2019-04-13
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Notifications.Infrastructure.Configurations
{
    public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure([NotNull] EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable(nameof(NotificationsDbContext.Notifications));

            builder.Property(notification => notification.Id)
                   .HasConversion(notificationId => notificationId.ToGuid(), value => NotificationId.FromGuid(value))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(notification => notification.Timestamp).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(notification => notification.Title).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(notification => notification.Message).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(notification => notification.RedirectUrl).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(notification => notification.IsRead).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<UserId>(nameof(UserId)).HasConversion(userId => userId.ToGuid(), value => UserId.FromGuid(value)).IsRequired();

            builder.HasKey(notification => notification.Id);
        }
    }
}