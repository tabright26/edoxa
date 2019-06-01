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
    [Migration("20190531052921_InitialCreate")]
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

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Account", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Transaction", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("AccountId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Failure");

                    b.Property<int>("Status");

                    b.Property<DateTime>("Timestamp");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("BankAccountId");

                    b.Property<string>("ConnectAccountId")
                        .IsRequired();

                    b.Property<string>("CustomerId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Account", b =>
                {
                    b.HasOne("eDoxa.Cashier.Domain.AggregateModels.UserAggregate.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Account", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Transaction", b =>
                {
                    b.HasOne("eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Currency", "Currency", b1 =>
                        {
                            b1.Property<Guid>("TransactionId");

                            b1.Property<decimal>("Amount");

                            b1.Property<int>("Type");

                            b1.HasKey("TransactionId");

                            b1.ToTable("Transactions","edoxa");

                            b1.HasOne("eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Transaction")
                                .WithOne("Currency")
                                .HasForeignKey("eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Currency", "TransactionId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}