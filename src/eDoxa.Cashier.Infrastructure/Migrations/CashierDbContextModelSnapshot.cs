﻿// Filename: CashierDbContextModelSnapshot.cs
// Date Created: 2019-04-24
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
            modelBuilder
                .HasDefaultSchema("edoxa")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.Account", b =>
            {
                b.Property<Guid>("Id");

                b.Property<Guid>("UserId");

                b.HasKey("Id");

                b.HasIndex("UserId")
                    .IsUnique();

                b.ToTable("Accounts");
            });

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.User", b =>
            {
                b.Property<Guid>("Id");

                b.Property<string>("CustomerId")
                    .IsRequired();

                b.HasKey("Id");

                b.ToTable("Users");
            });

            modelBuilder.Entity("eDoxa.Seedwork.Infrastructure.Repositories.RequestLogEntry", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<Guid?>("IdempotencyKey");

                b.Property<string>("LocalIpAddress");

                b.Property<string>("Method");

                b.Property<string>("Origin");

                b.Property<string>("RemoteIpAddress");

                b.Property<DateTime>("Time");

                b.Property<int>("Type");

                b.Property<string>("Url");

                b.Property<string>("Version");

                b.HasKey("Id");

                b.HasIndex("IdempotencyKey")
                    .IsUnique()
                    .HasFilter("[IdempotencyKey] IS NOT NULL");

                b.ToTable("RequestLogs", "dbo");
            });

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.Account", b =>
            {
                b.HasOne("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.User", "User")
                    .WithOne("Account")
                    .HasForeignKey("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.Account", "UserId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.OwnsOne("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.MoneyAccount", "Funds", b1 =>
                {
                    b1.Property<Guid>("AccountId");

                    b1.Property<decimal>("Balance");

                    b1.Property<decimal>("Pending");

                    b1.HasKey("AccountId");

                    b1.ToTable("Funds", "edoxa");

                    b1.HasOne("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.Account")
                        .WithOne("Funds")
                        .HasForeignKey("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.MoneyAccount", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

                b.OwnsOne("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.TokenAccount", "Tokens", b1 =>
                {
                    b1.Property<Guid>("AccountId");

                    b1.Property<long>("Balance");

                    b1.Property<long>("Pending");

                    b1.HasKey("AccountId");

                    b1.ToTable("Tokens", "edoxa");

                    b1.HasOne("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.Account")
                        .WithOne("Tokens")
                        .HasForeignKey("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.TokenAccount", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
            });
#pragma warning restore 612, 618
        }
    }
}