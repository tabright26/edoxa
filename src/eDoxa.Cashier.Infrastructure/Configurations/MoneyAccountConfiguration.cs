﻿// Filename: MoneyAccountConfiguration.cs
// Date Created: 2019-04-24
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
    internal sealed class MoneyAccountConfiguration : IEntityTypeConfiguration<MoneyAccount>
    {
        public void Configure([NotNull] EntityTypeBuilder<MoneyAccount> builder)
        {
            builder.ToTable(nameof(CashierDbContext.MoneyAccounts));

            builder.Property(account => account.Id)
                .HasConversion(accountId => accountId.ToGuid(), accountId => AccountId.FromGuid(accountId))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<UserId>(nameof(UserId))
                .HasConversion(userId => userId.ToGuid(), userId => UserId.FromGuid(userId))
                .IsRequired();

            builder.Ignore(account => account.Balance);

            builder.Property(account => account.Pending)
                .HasConversion<decimal>(account => account, amount => new Money(amount))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(account => account.Transactions)
                .WithOne(transaction => transaction.Account)
                .HasForeignKey(nameof(AccountId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(account => account.Id);

            builder.Metadata.FindNavigation(nameof(MoneyAccount.Transactions)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}