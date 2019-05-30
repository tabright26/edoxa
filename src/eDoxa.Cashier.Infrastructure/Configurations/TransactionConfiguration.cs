// Filename: TransactionConfiguration.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;
using eDoxa.Seedwork.Infrastructure.Extensions;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure([NotNull] EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable(nameof(CashierDbContext.Transactions));

            builder.EntityId(transaction => transaction.Id).IsRequired();

            builder.Property<AccountId>(nameof(AccountId))
                .HasConversion(accountId => accountId.ToGuid(), accountId => AccountId.FromGuid(accountId))
                .IsRequired();

            builder.Property(transaction => transaction.Timestamp).IsRequired();

            builder.OwnsOne(transaction => transaction.Currency)
                .Property(currency => currency.Type)
                .HasConversion(type => type.Value, value => CurrencyType.FromValue(value))
                .IsRequired();

            builder.OwnsOne(transaction => transaction.Currency).Property(currency => currency.Amount).IsRequired();

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
