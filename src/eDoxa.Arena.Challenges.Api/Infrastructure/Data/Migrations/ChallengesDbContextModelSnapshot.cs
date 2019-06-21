﻿// Filename: ChallengesDbContextModelSnapshot.cs
// Date Created: 2019-06-20
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
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Infrastructure.Models.ChallengeModel",
                b =>
                {
                    b.Property<Guid>("Id").ValueGeneratedOnAdd();

                    b.Property<int>("Game");

                    b.Property<DateTime?>("LastSync");

                    b.Property<string>("Name");

                    b.Property<int?>("Seed");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("Challenge");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Infrastructure.Models.MatchModel",
                b =>
                {
                    b.Property<Guid>("Id").ValueGeneratedOnAdd();

                    b.Property<string>("GameMatchId");

                    b.Property<Guid>("ParticipantId");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("GameMatchId").IsUnique().HasFilter("[GameMatchId] IS NOT NULL");

                    b.HasIndex("ParticipantId");

                    b.ToTable("Match");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Infrastructure.Models.ParticipantModel",
                b =>
                {
                    b.Property<Guid>("Id").ValueGeneratedOnAdd();

                    b.Property<Guid>("ChallengeId");

                    b.Property<string>("GameAccountId");

                    b.Property<DateTime?>("LastSync");

                    b.Property<DateTime>("Timestamp");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.HasIndex("Id", "UserId").IsUnique();

                    b.ToTable("Participant");
                }
            );

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Infrastructure.Models.ChallengeModel",
                b =>
                {
                    b.OwnsMany(
                        "eDoxa.Arena.Challenges.Infrastructure.Models.BucketModel",
                        "Buckets",
                        b1 =>
                        {
                            b1.Property<Guid>("ChallengeId");

                            b1.Property<Guid>("Id").ValueGeneratedOnAdd();

                            b1.Property<decimal>("PrizeAmount");

                            b1.Property<int>("PrizeCurrency");

                            b1.Property<int>("Size");

                            b1.HasKey("ChallengeId", "Id");

                            b1.ToTable("Bucket");

                            b1.HasOne("eDoxa.Arena.Challenges.Infrastructure.Models.ChallengeModel")
                                .WithMany("Buckets")
                                .HasForeignKey("ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);
                        }
                    );

                    b.OwnsMany(
                        "eDoxa.Arena.Challenges.Infrastructure.Models.ScoringItemModel",
                        "ScoringItems",
                        b1 =>
                        {
                            b1.Property<Guid>("ChallengeId");

                            b1.Property<Guid>("Id").ValueGeneratedOnAdd();

                            b1.Property<string>("Name");

                            b1.Property<float>("Weighting");

                            b1.HasKey("ChallengeId", "Id");

                            b1.ToTable("ScoringItem");

                            b1.HasOne("eDoxa.Arena.Challenges.Infrastructure.Models.ChallengeModel")
                                .WithMany("ScoringItems")
                                .HasForeignKey("ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);
                        }
                    );

                    b.OwnsOne(
                        "eDoxa.Arena.Challenges.Infrastructure.Models.SetupModel",
                        "Setup",
                        b1 =>
                        {
                            b1.Property<Guid>("ChallengeModelId");

                            b1.Property<int>("BestOf");

                            b1.Property<int>("Entries");

                            b1.Property<decimal>("EntryFeeAmount");

                            b1.Property<int>("EntryFeeCurrency");

                            b1.Property<int>("PayoutEntries");

                            b1.HasKey("ChallengeModelId");

                            b1.ToTable("Challenge");

                            b1.HasOne("eDoxa.Arena.Challenges.Infrastructure.Models.ChallengeModel")
                                .WithOne("Setup")
                                .HasForeignKey("eDoxa.Arena.Challenges.Infrastructure.Models.SetupModel", "ChallengeModelId")
                                .OnDelete(DeleteBehavior.Cascade);
                        }
                    );

                    b.OwnsOne(
                        "eDoxa.Arena.Challenges.Infrastructure.Models.TimelineModel",
                        "Timeline",
                        b1 =>
                        {
                            b1.Property<Guid>("ChallengeModelId");

                            b1.Property<DateTime?>("ClosedAt");

                            b1.Property<long>("Duration");

                            b1.Property<DateTime?>("StartedAt");

                            b1.HasKey("ChallengeModelId");

                            b1.ToTable("Challenge");

                            b1.HasOne("eDoxa.Arena.Challenges.Infrastructure.Models.ChallengeModel")
                                .WithOne("Timeline")
                                .HasForeignKey("eDoxa.Arena.Challenges.Infrastructure.Models.TimelineModel", "ChallengeModelId")
                                .OnDelete(DeleteBehavior.Cascade);
                        }
                    );
                }
            );

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Infrastructure.Models.MatchModel",
                b =>
                {
                    b.HasOne("eDoxa.Arena.Challenges.Infrastructure.Models.ParticipantModel", "Participant")
                        .WithMany("Matches")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsMany(
                        "eDoxa.Arena.Challenges.Infrastructure.Models.StatModel",
                        "Stats",
                        b1 =>
                        {
                            b1.Property<Guid>("MatchId");

                            b1.Property<Guid>("Id").ValueGeneratedOnAdd();

                            b1.Property<string>("Name");

                            b1.Property<double>("Value");

                            b1.Property<float>("Weighting");

                            b1.HasKey("MatchId", "Id");

                            b1.ToTable("Stat");

                            b1.HasOne("eDoxa.Arena.Challenges.Infrastructure.Models.MatchModel")
                                .WithMany("Stats")
                                .HasForeignKey("MatchId")
                                .OnDelete(DeleteBehavior.Cascade);
                        }
                    );
                }
            );

            modelBuilder.Entity(
                "eDoxa.Arena.Challenges.Infrastructure.Models.ParticipantModel",
                b =>
                {
                    b.HasOne("eDoxa.Arena.Challenges.Infrastructure.Models.ChallengeModel", "Challenge")
                        .WithMany("Participants")
                        .HasForeignKey("ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade);
                }
            );
#pragma warning restore 612, 618
        }
    }
}
