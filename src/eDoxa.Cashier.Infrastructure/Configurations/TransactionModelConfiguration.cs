// Filename: TransactionModelConfiguration.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Infrastructure.Models;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    internal sealed class TransactionModelConfiguration : IEntityTypeConfiguration<TransactionModel>
    {
        public void Configure([NotNull] EntityTypeBuilder<TransactionModel> builder)
        {
            builder.ToTable("Transaction");

            builder.Property(transaction => transaction.Id).IsRequired();

            builder.Property(transaction => transaction.Timestamp).IsRequired();

            builder.Property(transaction => transaction.Amount).HasColumnType("decimal(10, 2)").IsRequired();

            builder.Property(transaction => transaction.Currency).IsRequired();

            builder.Property(transaction => transaction.Type).IsRequired();

            builder.Property(transaction => transaction.Status).IsRequired();

            builder.Property(transaction => transaction.Description).IsRequired();

            builder.HasKey(transaction => transaction.Id);
        }
    }
}
