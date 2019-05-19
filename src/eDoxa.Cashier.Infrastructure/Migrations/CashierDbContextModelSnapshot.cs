// Filename: CashierDbContextModelSnapshot.cs
// Date Created: 2019-05-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eDoxa.Cashier.Infrastructure.Migrations
{
    [DbContext(typeof(CashierDbContext))]
    internal class CashierDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasDefaultSchema("edoxa")
                        .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                        .HasAnnotation("Relational:MaxIdentifierLength", 128)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity(
                "eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate.MoneyAccount",
                b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime?>("LastDeposit");

                    b.Property<DateTime?>("LastWithdraw");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("MoneyAccounts");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate.MoneyTransaction",
                b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("AccountId");

                    b.Property<decimal>("Amount");

                    b.Property<string>("Description").IsRequired();

                    b.Property<string>("Failure");

                    b.Property<int>("Status");

                    b.Property<DateTime>("Timestamp");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("MoneyTransactions");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate.TokenAccount",
                b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime?>("LastDeposit");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("TokenAccounts");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate.TokenTransaction",
                b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("AccountId");

                    b.Property<long>("Amount");

                    b.Property<string>("Description").IsRequired();

                    b.Property<string>("Failure");

                    b.Property<int>("Status");

                    b.Property<DateTime>("Timestamp");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("TokenTransactions");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Seedwork.Infrastructure.Models.LogEntry",
                b =>
                {
                    b.Property<Guid>("Id").ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<Guid?>("IdempotencyKey");

                    b.Property<string>("LocalIpAddress");

                    b.Property<string>("Method");

                    b.Property<string>("Origin");

                    b.Property<string>("RemoteIpAddress");

                    b.Property<string>("RequestBody");

                    b.Property<string>("RequestType");

                    b.Property<string>("ResponseBody");

                    b.Property<string>("ResponseType");

                    b.Property<string>("Url");

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.HasIndex("IdempotencyKey").IsUnique().HasFilter("[IdempotencyKey] IS NOT NULL");

                    b.ToTable("Logs", "dbo");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate.MoneyTransaction",
                b =>
                {
                    b.HasOne("eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate.MoneyAccount")
                     .WithMany("Transactions")
                     .HasForeignKey("AccountId")
                     .OnDelete(DeleteBehavior.Cascade);
                }
            );

            modelBuilder.Entity(
                "eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate.TokenTransaction",
                b =>
                {
                    b.HasOne("eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate.TokenAccount")
                     .WithMany("Transactions")
                     .HasForeignKey("AccountId")
                     .OnDelete(DeleteBehavior.Cascade);
                }
            );
#pragma warning restore 612, 618
        }
    }
}
