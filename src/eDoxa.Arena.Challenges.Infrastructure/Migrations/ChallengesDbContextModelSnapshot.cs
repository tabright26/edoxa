﻿// Filename: ChallengesDbContextModelSnapshot.cs
// Date Created: 2019-06-07
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

namespace eDoxa.Arena.Challenges.Infrastructure.Migrations
{
    [DbContext(typeof(ChallengesDbContext))]
    internal class ChallengesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasDefaultSchema("edoxa")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge",
                b =>
                {
                    b.Property<Guid>("Id").HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt").HasColumnName("CreatedAt");

                    b.Property<int>("Game").HasColumnName("Game");

                    b.Property<DateTime?>("LastSync").HasColumnName("LastSync");

                    b.Property<string>("Name").IsRequired().HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Challenge");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate.Match",
                b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("MatchReference").IsRequired();

                    b.Property<Guid>("ParticipantId");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.ToTable("Match");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate.Participant",
                b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("ChallengeId");

                    b.Property<string>("ExternalAccount").IsRequired();

                    b.Property<DateTime?>("LastSync");

                    b.Property<DateTime>("Timestamp");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.ToTable("Participant");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge",
                b =>
                {
                    b.OwnsOne(
                        "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup",
                        "Setup",
                        b1 =>
                        {
                            b1.Property<Guid>("ChallengeId");

                            b1.HasKey("ChallengeId");

                            b1.ToTable("Setup", "edoxa");

                            b1.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge")
                                .WithOne("Setup")
                                .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup", "ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne(
                                "eDoxa.Arena.Challenges.Domain.AggregateModels.BestOf",
                                "BestOf",
                                b2 =>
                                {
                                    b2.Property<Guid>("ChallengeSetupChallengeId");

                                    b2.Property<int>("Value").HasColumnName("BestOf");

                                    b2.HasKey("ChallengeSetupChallengeId");

                                    b2.ToTable("Setup", "edoxa");

                                    b2.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup")
                                        .WithOne("BestOf")
                                        .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.BestOf", "ChallengeSetupChallengeId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                }
                            );

                            b1.OwnsOne(
                                "eDoxa.Arena.Challenges.Domain.AggregateModels.Entries",
                                "Entries",
                                b2 =>
                                {
                                    b2.Property<Guid>("ChallengeSetupChallengeId");

                                    b2.Property<int>("Value").HasColumnName("Entries");

                                    b2.HasKey("ChallengeSetupChallengeId");

                                    b2.ToTable("Setup", "edoxa");

                                    b2.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup")
                                        .WithOne("Entries")
                                        .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.Entries", "ChallengeSetupChallengeId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                }
                            );

                            b1.OwnsOne(
                                "eDoxa.Arena.Challenges.Domain.AggregateModels.PayoutEntries",
                                "PayoutEntries",
                                b2 =>
                                {
                                    b2.Property<Guid>("ChallengeSetupChallengeId");

                                    b2.Property<int>("Value").HasColumnName("PayoutEntries");

                                    b2.HasKey("ChallengeSetupChallengeId");

                                    b2.ToTable("Setup", "edoxa");

                                    b2.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup")
                                        .WithOne("PayoutEntries")
                                        .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.PayoutEntries", "ChallengeSetupChallengeId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                }
                            );

                            b1.OwnsOne(
                                "eDoxa.Arena.Challenges.Domain.AggregateModels.PrizePool",
                                "PrizePool",
                                b2 =>
                                {
                                    b2.Property<Guid>("ChallengeSetupChallengeId");

                                    b2.Property<decimal>("Amount").HasColumnName("PrizePoolAmount");

                                    b2.Property<int>("Type").HasColumnName("PrizePoolCurrency");

                                    b2.HasKey("ChallengeSetupChallengeId");

                                    b2.ToTable("Setup", "edoxa");

                                    b2.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup")
                                        .WithOne("PrizePool")
                                        .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.PrizePool", "ChallengeSetupChallengeId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                }
                            );

                            b1.OwnsOne(
                                "eDoxa.Arena.Domain.ValueObjects.EntryFee",
                                "EntryFee",
                                b2 =>
                                {
                                    b2.Property<Guid>("ChallengeSetupChallengeId");

                                    b2.Property<decimal>("Amount").HasColumnName("EntryFeeAmount");

                                    b2.Property<int>("Type").HasColumnName("EntryFeeCurrency");

                                    b2.HasKey("ChallengeSetupChallengeId");

                                    b2.ToTable("Setup", "edoxa");

                                    b2.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup")
                                        .WithOne("EntryFee")
                                        .HasForeignKey("eDoxa.Arena.Domain.ValueObjects.EntryFee", "ChallengeSetupChallengeId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                }
                            );
                        }
                    );

                    b.OwnsMany(
                        "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeStat",
                        "Stats",
                        b1 =>
                        {
                            b1.Property<Guid>("ChallengeId").HasColumnName("ChallengeId");

                            b1.Property<Guid>("Id").ValueGeneratedOnAdd();

                            b1.Property<string>("Name").IsRequired().HasColumnName("Name");

                            b1.Property<float>("Weighting").HasColumnName("Weighting");

                            b1.HasKey("ChallengeId", "Id");

                            b1.ToTable("Scoring");

                            b1.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge")
                                .WithMany("Stats")
                                .HasForeignKey("ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);
                        }
                    );

                    b.OwnsOne(
                        "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeTimeline",
                        "Timeline",
                        b1 =>
                        {
                            b1.Property<Guid>("ChallengeId");

                            b1.Property<DateTime?>("ClosedAt").HasColumnName("ClosedAt");

                            b1.Property<DateTime?>("StartedAt").HasColumnName("StartedAt");

                            b1.HasKey("ChallengeId");

                            b1.ToTable("Timeline", "edoxa");

                            b1.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge")
                                .WithOne("Timeline")
                                .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeTimeline", "ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne(
                                "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeDuration",
                                "Duration",
                                b2 =>
                                {
                                    b2.Property<Guid>("ChallengeTimelineChallengeId");

                                    b2.Property<long>("Ticks").HasColumnName("Duration");

                                    b2.HasKey("ChallengeTimelineChallengeId");

                                    b2.ToTable("Timeline", "edoxa");

                                    b2.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeTimeline")
                                        .WithOne("Duration")
                                        .HasForeignKey(
                                            "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeDuration",
                                            "ChallengeTimelineChallengeId"
                                        )
                                        .OnDelete(DeleteBehavior.Cascade);
                                }
                            );
                        }
                    );

                    b.OwnsOne(
                        "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.TestMode",
                        "TestMode",
                        b1 =>
                        {
                            b1.Property<Guid>("ChallengeId");

                            b1.Property<int>("MatchQuantity").HasColumnName("AverageBestOf");

                            b1.Property<int>("ParticipantQuantity").HasColumnName("ParticipantQuantity");

                            b1.Property<int>("StartingState").HasColumnName("State");

                            b1.HasKey("ChallengeId");

                            b1.ToTable("TestMode", "edoxa");

                            b1.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge")
                                .WithOne("TestMode")
                                .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.TestMode", "ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);
                        }
                    );

                    b.OwnsMany(
                        "eDoxa.Arena.Domain.Bucket",
                        "Buckets",
                        b1 =>
                        {
                            b1.Property<Guid>("ChallengeId").HasColumnName("ChallengeId");

                            b1.Property<Guid>("Id").ValueGeneratedOnAdd();

                            b1.HasKey("ChallengeId", "Id");

                            b1.ToTable("Payout");

                            b1.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge")
                                .WithMany("Buckets")
                                .HasForeignKey("ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne(
                                "eDoxa.Arena.Domain.BucketSize",
                                "Size",
                                b2 =>
                                {
                                    b2.Property<Guid>("BucketChallengeId");

                                    b2.Property<Guid>("BucketId");

                                    b2.Property<int>("Value").HasColumnName("Size");

                                    b2.HasKey("BucketChallengeId", "BucketId");

                                    b2.ToTable("Payout", "edoxa");

                                    b2.HasOne("eDoxa.Arena.Domain.Bucket")
                                        .WithOne("Size")
                                        .HasForeignKey("eDoxa.Arena.Domain.BucketSize", "BucketChallengeId", "BucketId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                }
                            );

                            b1.OwnsOne(
                                "eDoxa.Arena.Domain.ValueObjects.Prize",
                                "Prize",
                                b2 =>
                                {
                                    b2.Property<Guid>("BucketChallengeId");

                                    b2.Property<Guid>("BucketId");

                                    b2.Property<decimal>("Amount").HasColumnName("PrizeAmount");

                                    b2.Property<int>("Type").HasColumnName("PrizeCurrency");

                                    b2.HasKey("BucketChallengeId", "BucketId");

                                    b2.ToTable("Payout", "edoxa");

                                    b2.HasOne("eDoxa.Arena.Domain.Bucket")
                                        .WithOne("Prize")
                                        .HasForeignKey("eDoxa.Arena.Domain.ValueObjects.Prize", "BucketChallengeId", "BucketId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                }
                            );
                        }
                    );
                }
            );

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate.Match",
                b =>
                {
                    b.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate.Participant", "Participant")
                        .WithMany("Matches")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsMany(
                        "eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate.Stat",
                        "Stats",
                        b1 =>
                        {
                            b1.Property<Guid>("MatchId").HasColumnName("MatchId");

                            b1.Property<Guid>("Id").ValueGeneratedOnAdd();

                            b1.Property<string>("Name").IsRequired().HasColumnName("Name");

                            b1.Property<double>("Value").HasColumnName("Value");

                            b1.Property<float>("Weighting").HasColumnName("Weighting");

                            b1.HasKey("MatchId", "Id");

                            b1.ToTable("Stat");

                            b1.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate.Match")
                                .WithMany("Stats")
                                .HasForeignKey("MatchId")
                                .OnDelete(DeleteBehavior.Cascade);
                        }
                    );
                }
            );

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate.Participant",
                b =>
                {
                    b.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge", "Challenge")
                        .WithMany("Participants")
                        .HasForeignKey("ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade);
                }
            );
#pragma warning restore 612, 618
        }
    }
}
