// Filename: TokenTransactionConfiguration.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    internal sealed class TokenTransactionConfiguration : IEntityTypeConfiguration<TokenTransaction>
    {
        public void Configure([NotNull] EntityTypeBuilder<TokenTransaction> builder)
        {
            builder.ToTable(nameof(CashierDbContext.TokenTransactions));

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
                .HasConversion<long>(token => token, token => new Token(token))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(transaction => transaction.Description)
                .HasConversion(description => description.ToString(), description => new TransactionDescription(description, false))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(transaction => transaction.Type)
                .HasConversion(type => type.Value, value => TransactionType.FromValue(value))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(transaction => transaction.Status)
                .HasConversion(type => type.Value, value => TransactionStatus.FromValue(value))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(transaction => transaction.ServiceId)
                .IsRequired(false)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(transaction => transaction.Id);
        }
    }
}