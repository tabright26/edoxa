﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eDoxa.Arena.Challenges.Infrastructure;

namespace eDoxa.Arena.Challenges.Infrastructure.Migrations
{
    [DbContext(typeof(ChallengesDbContext))]
    [Migration("20190529225122_InitialCreate")]
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

            modelBuilder.Entity("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("Game");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Scoring")
                        .IsRequired();

                    b.Property<bool>("TestMode");

                    b.HasKey("Id");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate.Match", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("MatchExternalId")
                        .IsRequired();

                    b.Property<Guid>("ParticipantId");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate.Stat", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("MatchId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Value");

                    b.Property<float>("Weighting");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate.Participant", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("ChallengeId");

                    b.Property<string>("ExternalAccount")
                        .IsRequired();

                    b.Property<DateTime>("Timestamp");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge", b =>
                {
                    b.OwnsOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup", "Setup", b1 =>
                        {
                            b1.Property<Guid>("ChallengeId");

                            b1.Property<int>("BestOf");

                            b1.Property<int>("Entries");

                            b1.Property<float>("PayoutRatio");

                            b1.Property<float>("ServiceChargeRatio");

                            b1.HasKey("ChallengeId");

                            b1.ToTable("Challenges","edoxa");

                            b1.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge")
                                .WithOne("Setup")
                                .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup", "ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("eDoxa.Arena.Domain.EntryFee", "EntryFee", b2 =>
                                {
                                    b2.Property<Guid>("ChallengeSetupChallengeId");

                                    b2.Property<decimal>("Amount");

                                    b2.Property<int>("Currency");

                                    b2.HasKey("ChallengeSetupChallengeId");

                                    b2.ToTable("Challenges","edoxa");

                                    b2.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup")
                                        .WithOne("EntryFee")
                                        .HasForeignKey("eDoxa.Arena.Domain.EntryFee", "ChallengeSetupChallengeId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });

                    b.OwnsOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeTimeline", "Timeline", b1 =>
                        {
                            b1.Property<Guid>("ChallengeId");

                            b1.Property<DateTime?>("ClosedAt");

                            b1.Property<long>("Duration");

                            b1.Property<DateTime?>("StartedAt");

                            b1.HasKey("ChallengeId");

                            b1.ToTable("Challenges","edoxa");

                            b1.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge")
                                .WithOne("Timeline")
                                .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeTimeline", "ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate.Match", b =>
                {
                    b.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate.Participant", "Participant")
                        .WithMany("Matches")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate.Stat", b =>
                {
                    b.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate.Match")
                        .WithMany("Stats")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate.Participant", b =>
                {
                    b.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge", "Challenge")
                        .WithMany("Participants")
                        .HasForeignKey("ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}