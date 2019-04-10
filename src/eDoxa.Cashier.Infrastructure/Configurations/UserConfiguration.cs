// Filename: UserConfiguration.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(CashierDbContext.Users));

            builder.Property(user => user.Id)
                   .HasConversion(userId => userId.ToGuid(), value => UserId.FromGuid(value))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(user => user.CustomerId)
                   .HasConversion(customerId => customerId.ToString(), input => CustomerId.Parse(input))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(user => user.Id);

            builder.HasOne(user => user.Account).WithOne(account => account.User).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(User.Account)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}