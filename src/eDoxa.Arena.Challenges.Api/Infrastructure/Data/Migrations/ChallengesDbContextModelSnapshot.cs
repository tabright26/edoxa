﻿// Filename: ChallengesDbContextModelSnapshot.cs
// Date Created: 2019-06-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Migrations
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
                    b.Property<Guid>("Id").HasColumnName("Id");

                    b.Property<Guid>("ParticipantId").HasColumnName("ParticipantId");

                    b.Property<string>("Reference").IsRequired().HasColumnName("Reference");

                    b.Property<DateTime>("Timestamp").HasColumnName("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.HasIndex("Reference").IsUnique();

                    b.ToTable("Match");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate.Participant",
                b =>
                {
                    b.Property<Guid>("Id").HasColumnName("Id");

                    b.Property<Guid>("ChallengeId").HasColumnName("ChallengeId");

                    b.Property<DateTime?>("LastSync").HasColumnName("LastSync");

                    b.Property<DateTime>("Timestamp").HasColumnName("Timestamp");

                    b.Property<string>("UserGameReference").IsRequired().HasColumnName("UserGameReference");

                    b.Property<Guid>("UserId").HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.HasIndex("Id", "UserId").IsUnique();

                    b.ToTable("Participant");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge",
                b =>
                {
                    b.OwnsMany(
                        "eDoxa.Arena.Challenges.Domain.AggregateModels.Bucket",
                        "Buckets",
                        b1 =>
                        {
                            b1.Property<Guid>("ChallengeId").HasColumnName("ChallengeId");

                            b1.Property<Guid>("Id").ValueGeneratedOnAdd();

                            b1.HasKey("ChallengeId", "Id");

                            b1.ToTable("Bucket");

                            b1.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge")
                                .WithMany("Buckets")
                                .HasForeignKey("ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne(
                                "eDoxa.Arena.Challenges.Domain.AggregateModels.BucketSize",
                                "Size",
                                b2 =>
                                {
                                    b2.Property<Guid>("BucketChallengeId");

                                    b2.Property<Guid>("BucketId");

                                    b2.Property<int>("Value").HasColumnName("Size");

                                    b2.HasKey("BucketChallengeId", "BucketId");

                                    b2.ToTable("Bucket", "edoxa");

                                    b2.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.Bucket")
                                        .WithOne("Size")
                                        .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.BucketSize", "BucketChallengeId", "BucketId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                }
                            );

                            b1.OwnsOne(
                                "eDoxa.Arena.Challenges.Domain.AggregateModels.Prize",
                                "Prize",
                                b2 =>
                                {
                                    b2.Property<Guid>("BucketChallengeId");

                                    b2.Property<Guid>("BucketId");

                                    b2.Property<decimal>("Amount").HasColumnName("PrizeAmount");

                                    b2.Property<int>("Type").HasColumnName("PrizeCurrency");

                                    b2.HasKey("BucketChallengeId", "BucketId");

                                    b2.ToTable("Bucket", "edoxa");

                                    b2.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.Bucket")
                                        .WithOne("Prize")
                                        .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.Prize", "BucketChallengeId", "BucketId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                }
                            );
                        }
                    );

                    b.OwnsOne(
                        "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup",
                        "Setup",
                        b1 =>
                        {
                            b1.Property<Guid>("ChallengeId");

                            b1.Property<int>("BestOf").HasColumnName("BestOf");

                            b1.Property<int>("Entries").HasColumnName("Entries");

                            b1.Property<int>("PayoutEntries").HasColumnName("PayoutEntries");

                            b1.HasKey("ChallengeId");

                            b1.ToTable("Setup", "edoxa");

                            b1.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge")
                                .WithOne("Setup")
                                .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup", "ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne(
                                "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ValueObjects.EntryFee",
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
                                        .HasForeignKey(
                                            "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ValueObjects.EntryFee",
                                            "ChallengeSetupChallengeId"
                                        )
                                        .OnDelete(DeleteBehavior.Cascade);
                                }
                            );
                        }
                    );

                    b.OwnsOne(
                        "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeTimeline",
                        "Timeline",
                        b1 =>
                        {
                            b1.Property<Guid>("ChallengeId");

                            b1.Property<DateTime?>("ClosedAt").HasColumnName("ClosedAt");

                            b1.Property<long>("Duration").HasColumnName("Duration");

                            b1.Property<DateTime?>("StartedAt").HasColumnName("StartedAt");

                            b1.HasKey("ChallengeId");

                            b1.ToTable("Timeline", "edoxa");

                            b1.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge")
                                .WithOne("Timeline")
                                .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeTimeline", "ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);
                        }
                    );

                    b.OwnsMany(
                        "eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ScoringItem",
                        "ScoringItems",
                        b1 =>
                        {
                            b1.Property<Guid>("ChallengeId").HasColumnName("ChallengeId");

                            b1.Property<Guid>("Id").ValueGeneratedOnAdd();

                            b1.Property<string>("Name").IsRequired().HasColumnName("Name");

                            b1.Property<float>("Weighting").HasColumnName("Weighting");

                            b1.HasKey("ChallengeId", "Id");

                            b1.ToTable("ScoringItem");

                            b1.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge")
                                .WithMany("ScoringItems")
                                .HasForeignKey("ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);
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
