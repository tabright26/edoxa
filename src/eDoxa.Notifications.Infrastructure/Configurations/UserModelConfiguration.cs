// Filename: UserModelConfiguration.cs
// Date Created: 2019-12-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Notifications.Infrastructure.Configurations
{
    internal sealed class UserModelConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.Property(user => user.Id).HasConversion(userId => userId.ToGuid(), value => UserId.FromGuid(value)).IsRequired().ValueGeneratedNever();

            builder.Property(user => user.Email).IsRequired();

            builder.HasKey(user => user.Id);
        }
    }
}
