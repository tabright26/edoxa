﻿// <auto-generated />
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
            modelBuilder
                .HasDefaultSchema("edoxa")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime?>("CompletedAt");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("Duration");

                    b.Property<int>("Game");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Scoring")
                        .IsRequired();

                    b.Property<DateTime?>("StartedAt");

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

                    b.Property<string>("ParticipantExternalAccount")
                        .IsRequired();

                    b.Property<DateTime>("Timestamp");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("eDoxa.Seedwork.Infrastructure.Models.LogEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

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

                    b.HasIndex("IdempotencyKey")
                        .IsUnique()
                        .HasFilter("[IdempotencyKey] IS NOT NULL");

                    b.ToTable("Logs","dbo");
                });

            modelBuilder.Entity("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge", b =>
                {
                    b.OwnsOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup", "Setup", b1 =>
                        {
                            b1.Property<Guid>("ChallengeId");

                            b1.Property<int>("BestOf")
                                .HasColumnName("BestOf");

                            b1.Property<int>("Entries")
                                .HasColumnName("Entries");

                            b1.Property<decimal>("EntryFee")
                                .HasColumnName("EntryFee")
                                .HasColumnType("decimal(4,2)");

                            b1.Property<int>("EntryFeeCurrency")
                                .HasColumnName("EntryFeeCurrency");

                            b1.Property<int>("PayoutCurrency")
                                .HasColumnName("PayoutCurrency");

                            b1.Property<float>("PayoutRatio")
                                .HasColumnName("PayoutRatio");

                            b1.Property<float>("ServiceChargeRatio")
                                .HasColumnName("ServiceChargeRatio");

                            b1.HasKey("ChallengeId");

                            b1.ToTable("Challenges","edoxa");

                            b1.HasOne("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Challenge")
                                .WithOne("Setup")
                                .HasForeignKey("eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ChallengeSetup", "ChallengeId")
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
