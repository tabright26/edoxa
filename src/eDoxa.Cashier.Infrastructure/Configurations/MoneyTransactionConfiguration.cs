// Filename: MoneyTransactionConfiguration.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Seedwork.Infrastructure.Extensions;

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

            builder.EntityId(transaction => transaction.Id).IsRequired();

            builder.Property<AccountId>(nameof(AccountId))
                .HasConversion(accountId => accountId.ToGuid(), accountId => AccountId.FromGuid(accountId))
                .IsRequired();

            builder.Property(transaction => transaction.Timestamp).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(transaction => transaction.Amount).HasConversion<decimal>(money => money, money => new Money(money)).IsRequired();

            builder.Enumeration(transaction => transaction.Type).IsRequired();

            builder.Enumeration(transaction => transaction.Status).IsRequired();

            builder.Property(transaction => transaction.Description)
                .HasConversion(description => description.ToString(), description => new TransactionDescription(description))
                .IsRequired();

            builder.Property(transaction => transaction.Failure)
                .HasConversion(failure => failure != null ? failure.ToString() : null, message => message != null ? new TransactionFailure(message) : null)
                .IsRequired(false);

            builder.HasKey(transaction => transaction.Id);
        }
    }
}
