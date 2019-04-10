// Filename: NotificationConfiguration.cs
// Date Created: 2019-03-31
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Newtonsoft.Json;

namespace eDoxa.Notifications.Infrastructure.Configurations
{
    public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        private readonly NotificationProvider _provider = NotificationProvider.Instance;

        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable(nameof(NotificationsDbContext.Notifications));

            builder.Property(notification => notification.Id)
                   .HasConversion(notificationId => notificationId.ToGuid(), value => NotificationId.FromGuid(value))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(notification => notification.Timestamp).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(notification => notification.Title);

            builder.Ignore(notification => notification.Message);

            builder.Property(notification => notification.IsRead).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(notification => notification.RedirectUrl).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(notification => notification.Metadata)
                   .HasConversion(
                       metadata => JsonConvert.SerializeObject(metadata, Formatting.None),
                       metadata => JsonConvert.DeserializeObject<NotificationMetadata>(metadata)
                   )
                   .IsRequired(false)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<UserId>(nameof(UserId)).HasConversion(userId => userId.ToGuid(), value => UserId.FromGuid(value)).IsRequired();

            builder.Property<NotificationDescription>("Description")
                   .HasField("_description")
                   .HasConversion(description => description.Name, name => _provider.FindDescriptionByName(name))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(notification => notification.Id);
        }
    }
}