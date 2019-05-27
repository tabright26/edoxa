// Filename: UserConfiguration.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Infrastructure.Extensions;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    public  sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure([NotNull] EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(CashierDbContext.Users));

            builder.EntityId(user => user.Id)
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(user => user.AccountId)
                   .HasConversion(accountId => accountId.ToString(), accountId => new StripeAccountId(accountId))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(user => user.CustomerId)
                   .HasConversion(customerId => customerId.ToString(), customerId =>new StripeCustomerId(customerId))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(user => user.BankAccountId)
                   .HasConversion(bankAccountId => bankAccountId != null ? bankAccountId.ToString() : null, bankAccountId => bankAccountId != null ? new StripeBankAccountId(bankAccountId) : null)
                   .IsRequired(false)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(user => user.MoneyAccount).WithOne(account => account.User).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(user => user.TokenAccount).WithOne(account => account.User).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(user => user.Id);

            builder.Metadata.FindNavigation(nameof(User.MoneyAccount)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata.FindNavigation(nameof(User.TokenAccount)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
