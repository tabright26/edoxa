// Filename: UserConfiguration.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure([NotNull] EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(CashierDbContext.Users));

            builder.Property(user => user.Id)
                .HasConversion(userId => userId.ToGuid(), userId => UserId.FromGuid(userId))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(user => user.CustomerId)
                .HasConversion(customerId => customerId.ToString(), input => CustomerId.Parse(input))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(user => user.MoneyAccount).WithOne(account => account.User).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(user => user.TokenAccount).WithOne(account => account.User).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(user => user.Id);
        }
    }
}