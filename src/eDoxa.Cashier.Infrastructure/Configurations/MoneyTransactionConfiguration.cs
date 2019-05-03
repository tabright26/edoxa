// Filename: MoneyTransactionConfiguration.cs
// Date Created: 2019-04-25
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
    internal sealed class MoneyTransactionConfiguration : IEntityTypeConfiguration<MoneyTransaction>
    {
        public void Configure([NotNull] EntityTypeBuilder<MoneyTransaction> builder)
        {
            builder.ToTable(nameof(CashierDbContext.MoneyTransactions));

            builder.Property(transaction => transaction.Id)
                .HasConversion(transactionId => transactionId.ToGuid(), transactionId => TransactionId.FromGuid(transactionId))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<AccountId>(nameof(AccountId))
                .HasConversion(accountId => accountId.ToGuid(), accountId => AccountId.FromGuid(accountId))
                .IsRequired();

            builder.Property(transaction => transaction.Timestamp)
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(transaction => transaction.Amount)
                .HasConversion<decimal>(money => money, money => new Money(money))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(transaction => transaction.Pending)
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(transaction => transaction.LinkedId)
                .IsRequired(false)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(transaction => transaction.Id);
        }
    }
}