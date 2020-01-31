// Filename: TransactionModelConfiguration.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

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

            builder.Ignore(transaction => transaction.DomainEvents);

            builder.Property(transaction => transaction.Id).IsRequired().ValueGeneratedNever();

            builder.Property(transaction => transaction.Timestamp).IsRequired();

            builder.Property(transaction => transaction.Amount).IsRequired().HasColumnType("decimal(10, 2)");

            builder.Property(transaction => transaction.Currency).IsRequired();

            builder.Property(transaction => transaction.Type).IsRequired();

            builder.Property(transaction => transaction.Status).IsRequired();

            builder.Property(transaction => transaction.Description).IsRequired();

            builder.OwnsMany(
                transaction => transaction.Metadata,
                transactionMetadata =>
                {
                    transactionMetadata.ToTable("TransactionMetadata");

                    transactionMetadata.WithOwner().HasForeignKey(metadata => metadata.TransactionId);

                    transactionMetadata.Property(metadata => metadata.Id).IsRequired().ValueGeneratedOnAdd();

                    transactionMetadata.Property(metadata => metadata.TransactionId).IsRequired();

                    transactionMetadata.Property(metadata => metadata.Key).IsRequired();

                    transactionMetadata.Property(metadata => metadata.Value).IsRequired();

                    transactionMetadata.HasKey(
                        metadata => new
                        {
                            metadata.TransactionId,
                            metadata.Id
                        });
                });

            builder.HasKey(transaction => transaction.Id);
        }
    }
}
