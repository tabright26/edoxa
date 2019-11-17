﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eDoxa.Cashier.Infrastructure;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Migrations
{
    [DbContext(typeof(CashierDbContext))]
    [Migration("20191109021642_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eDoxa.Cashier.Infrastructure.Models.AccountModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("eDoxa.Cashier.Infrastructure.Models.ChallengeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("EntryFeeAmount")
                        .HasColumnType("decimal(11, 2)");

                    b.Property<int>("EntryFeeCurrency");

                    b.HasKey("Id");

                    b.ToTable("Challenge");
                });

            modelBuilder.Entity("eDoxa.Cashier.Infrastructure.Models.TransactionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountId");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("Currency");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.Property<DateTime>("Timestamp");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("eDoxa.Cashier.Infrastructure.Models.ChallengeModel", b =>
                {
                    b.OwnsMany("eDoxa.Cashier.Infrastructure.Models.BucketModel", "Buckets", b1 =>
                        {
                            b1.Property<Guid>("ChallengeId");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd();

                            b1.Property<decimal>("PrizeAmount")
                                .HasColumnType("decimal(11, 2)");

                            b1.Property<int>("PrizeCurrency");

                            b1.Property<int>("Size");

                            b1.HasKey("ChallengeId", "Id");

                            b1.ToTable("Bucket");

                            b1.HasOne("eDoxa.Cashier.Infrastructure.Models.ChallengeModel")
                                .WithMany("Buckets")
                                .HasForeignKey("ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("eDoxa.Cashier.Infrastructure.Models.TransactionModel", b =>
                {
                    b.HasOne("eDoxa.Cashier.Infrastructure.Models.AccountModel", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsMany("eDoxa.Cashier.Infrastructure.Models.TransactionMetadataModel", "Metadata", b1 =>
                        {
                            b1.Property<Guid>("TransactionId");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd();

                            b1.Property<string>("Key");

                            b1.Property<string>("Value");

                            b1.HasKey("TransactionId", "Id");

                            b1.ToTable("TransactionMetadata");

                            b1.HasOne("eDoxa.Cashier.Infrastructure.Models.TransactionModel")
                                .WithMany("Metadata")
                                .HasForeignKey("TransactionId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
