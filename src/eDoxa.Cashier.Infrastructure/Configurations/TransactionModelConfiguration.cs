// Filename: TransactionModelConfiguration.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    internal sealed class TransactionModelConfiguration : IEntityTypeConfiguration<TransactionModel>
    {
        public void Configure(EntityTypeBuilder<TransactionModel> builder)
        {
            builder.ToTable("Transaction");

            builder.Property(transaction => transaction.Id).IsRequired().ValueGeneratedNever();;

            builder.Property(transaction => transaction.Timestamp).IsRequired();

            builder.Property(transaction => transaction.Amount).HasColumnType("decimal(10, 2)").IsRequired();

            builder.Property(transaction => transaction.Currency).IsRequired();

            builder.Property(transaction => transaction.Type).IsRequired();

            builder.Property(transaction => transaction.Status).IsRequired();

            builder.Property(transaction => transaction.Description).IsRequired();

            builder.OwnsMany(
                transaction => transaction.Metadata,
                transactionMetadata =>
                {
                    transactionMetadata.ToTable("TransactionMetadata");

                    transactionMetadata.WithOwner().HasForeignKey("TransactionId");

                    transactionMetadata.Property<Guid>("Id").ValueGeneratedOnAdd();

                    transactionMetadata.HasKey("TransactionId", "Id");
                });

            builder.HasKey(transaction => transaction.Id);
        }
    }
}
