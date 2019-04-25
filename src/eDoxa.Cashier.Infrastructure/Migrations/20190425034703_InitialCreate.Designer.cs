﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eDoxa.Cashier.Infrastructure;

namespace eDoxa.Cashier.Infrastructure.Migrations
{
    [DbContext(typeof(CashierDbContext))]
    [Migration("20190425034703_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("edoxa")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.MoneyAccount", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<decimal>("Pending");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("MoneyAccounts");
                });

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.MoneyTransaction", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("AccountId");

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("MoneyTransactions");
                });

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.TokenAccount", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<long>("Pending");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("TokenAccounts");
                });

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.TokenTransaction", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("AccountId");

                    b.Property<long>("Amount");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("TokenTransactions");
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

                    b.ToTable("RequestLogs","dbo");
                });

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.MoneyAccount", b =>
                {
                    b.HasOne("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.User", "User")
                        .WithOne("Funds")
                        .HasForeignKey("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.MoneyAccount", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.MoneyTransaction", b =>
                {
                    b.HasOne("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.MoneyAccount", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.TokenAccount", b =>
                {
                    b.HasOne("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.User", "User")
                        .WithOne("Tokens")
                        .HasForeignKey("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.TokenAccount", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.TokenTransaction", b =>
                {
                    b.HasOne("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.TokenAccount", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
