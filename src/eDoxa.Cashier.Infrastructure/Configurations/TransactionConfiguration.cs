// Filename: TransactionConfiguration.cs
// Date Created: 2019-04-17
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
    internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure([NotNull] EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable(nameof(CashierDbContext.Transactions));

            builder.Property(transaction => transaction.Id)
                .HasConversion(transactionId => transactionId.ToGuid(), value => TransactionId.FromGuid(value))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<UserId>(nameof(UserId)).HasConversion(userId => userId.ToGuid(), value => UserId.FromGuid(value)).IsRequired();

            builder.Property(transaction => transaction.Price)
                .HasConversion<decimal>(price => price, price => new Money(price)).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(transaction => transaction.Description)
                .HasConversion(description => description.ToString(), description => new TransactionDescription(description)).IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(transaction => transaction.Type).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(transaction => transaction.Id);
        }
    }
}