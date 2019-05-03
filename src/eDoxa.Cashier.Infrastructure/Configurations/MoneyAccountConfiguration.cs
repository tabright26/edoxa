// Filename: MoneyAccountConfiguration.cs
// Date Created: 2019-04-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;

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

            builder.Property(account => account.UserId)
                .HasConversion(userId => userId.ToGuid(), userId => UserId.FromGuid(userId))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(account => account.Balance);

            builder.Ignore(account => account.Pending);

            builder.HasMany(account => account.Transactions)
                .WithOne()
                .HasForeignKey(nameof(AccountId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(account => account.Id);

            builder.Metadata.FindNavigation(nameof(MoneyAccount.Transactions)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}