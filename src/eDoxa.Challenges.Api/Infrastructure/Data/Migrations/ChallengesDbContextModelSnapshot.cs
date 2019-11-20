﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

using eDoxa.Challenges.Infrastructure;

namespace eDoxa.Challenges.Api.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ChallengesDbContext))]
    internal class ChallengesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eDoxa.Challenges.Infrastructure.Models.ChallengeModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BestOf");

                    b.Property<int>("Entries");

                    b.Property<int>("Game");

                    b.Property<string>("Name");

                    b.Property<int>("State");

                    b.Property<DateTime?>("SynchronizedAt");

                    b.HasKey("Id");

                    b.ToTable("Challenge");
                });

            modelBuilder.Entity("eDoxa.Challenges.Infrastructure.Models.MatchModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameUuid");

                    b.Property<Guid?>("ParticipantId");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("eDoxa.Challenges.Infrastructure.Models.ParticipantModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ChallengeId");

                    b.Property<string>("PlayerId");

                    b.Property<DateTime>("RegisteredAt");

                    b.Property<DateTime?>("SynchronizedAt");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.ToTable("Participant");
                });

            modelBuilder.Entity("eDoxa.Challenges.Infrastructure.Models.ChallengeModel", b =>
                {
                    b.OwnsOne("eDoxa.Challenges.Infrastructure.Models.ChallengeTimelineModel", "Timeline", b1 =>
                        {
                            b1.Property<Guid>("ChallengeModelId");

                            b1.Property<DateTime?>("ClosedAt");

                            b1.Property<DateTime>("CreatedAt");

                            b1.Property<long>("Duration");

                            b1.Property<DateTime?>("StartedAt");

                            b1.HasKey("ChallengeModelId");

                            b1.ToTable("Challenge");

                            b1.HasOne("eDoxa.Challenges.Infrastructure.Models.ChallengeModel")
                                .WithOne("Timeline")
                                .HasForeignKey("eDoxa.Challenges.Infrastructure.Models.ChallengeTimelineModel", "ChallengeModelId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsMany("eDoxa.Challenges.Infrastructure.Models.ScoringItemModel", "ScoringItems", b1 =>
                        {
                            b1.Property<Guid>("ChallengeId");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd();

                            b1.Property<string>("Name");

                            b1.Property<float>("Weighting");

                            b1.HasKey("ChallengeId", "Id");

                            b1.ToTable("ScoringItem");

                            b1.HasOne("eDoxa.Challenges.Infrastructure.Models.ChallengeModel")
                                .WithMany("ScoringItems")
                                .HasForeignKey("ChallengeId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("eDoxa.Challenges.Infrastructure.Models.MatchModel", b =>
                {
                    b.HasOne("eDoxa.Challenges.Infrastructure.Models.ParticipantModel", "Participant")
                        .WithMany("Matches")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsMany("eDoxa.Challenges.Infrastructure.Models.StatModel", "Stats", b1 =>
                        {
                            b1.Property<Guid>("MatchId");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd();

                            b1.Property<string>("Name");

                            b1.Property<double>("Value");

                            b1.Property<float>("Weighting");

                            b1.HasKey("MatchId", "Id");

                            b1.ToTable("Stat");

                            b1.HasOne("eDoxa.Challenges.Infrastructure.Models.MatchModel")
                                .WithMany("Stats")
                                .HasForeignKey("MatchId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("eDoxa.Challenges.Infrastructure.Models.ParticipantModel", b =>
                {
                    b.HasOne("eDoxa.Challenges.Infrastructure.Models.ChallengeModel", "Challenge")
                        .WithMany("Participants")
                        .HasForeignKey("ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}