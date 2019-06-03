﻿// Filename: ChallengeConfiguration.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Seedwork.Infrastructure.Extensions;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Arena.Challenges.Infrastructure.Configurations
{
    public sealed class ChallengeConfiguration : IEntityTypeConfiguration<Challenge>
    {
        public void Configure([NotNull] EntityTypeBuilder<Challenge> builder)
        {
            builder.ToTable("Challenge");

            builder.EntityId(challenge => challenge.Id).HasColumnName("Id").IsRequired();

            builder.Property(challenge => challenge.CreatedAt).HasColumnName("CreatedAt").IsRequired();

            builder.Property(challenge => challenge.Name)
                .HasConversion(name => name.ToString(), name => new ChallengeName(name))
                .HasColumnName("Name")
                .IsRequired();

            builder.Enumeration(challenge => challenge.Game).HasColumnName("Game").IsRequired();

            builder.OwnsOne(
                challenge => challenge.Setup,
                challengeSetup =>
                {
                    challengeSetup.ToTable("Setup");

                    challengeSetup.OwnsOne(setup => setup.BestOf).Property(bestOf => bestOf.Value).HasColumnName("BestOf").IsRequired();

                    challengeSetup.OwnsOne(setup => setup.Entries).Property(entries => entries.Value).HasColumnName("Entries").IsRequired();

                    challengeSetup.OwnsOne(setup => setup.PayoutRatio).Property(payoutRatio => payoutRatio.Value).HasColumnName("PayoutRatio").IsRequired();

                    challengeSetup.OwnsOne(setup => setup.ServiceChargeRatio)
                        .Property(serviceChargeRatio => serviceChargeRatio.Value)
                        .HasColumnName("ServiceChargeRatio")
                        .IsRequired();

                    challengeSetup.OwnsOne(challenge => challenge.EntryFee).Property(entryFee => entryFee.Amount).HasColumnName("EntryFee").IsRequired();

                    challengeSetup.OwnsOne(challenge => challenge.EntryFee).Enumeration(entryFee => entryFee.Type).HasColumnName("Currency").IsRequired();

                    challengeSetup.Ignore(setup => setup.PayoutEntries);

                    challengeSetup.Ignore(setup => setup.PrizePool);
                }
            );

            builder.OwnsOne(
                challenge => challenge.Timeline,
                challengeTimeline =>
                {
                    challengeTimeline.ToTable("Timeline");

                    challengeTimeline.OwnsOne(challenge => challenge.Duration).Property(duration => duration.Ticks).HasColumnName("Duration").IsRequired();

                    challengeTimeline.Property(challenge => challenge.StartedAt).HasColumnName("StartedAt").IsRequired(false);

                    challengeTimeline.Ignore(challenge => challenge.EndedAt);

                    challengeTimeline.Property(challenge => challenge.ClosedAt).HasColumnName("ClosedAt").IsRequired(false);

                    challengeTimeline.Ignore(challenge => challenge.State);
                }
            );

            builder.OwnsOne(
                challenge => challenge.TestMode,
                challengeTestMode =>
                {
                    challengeTestMode.ToTable("TestMode");

                    challengeTestMode.Property(challenge => challenge.StartingState)
                        .HasConversion(state => state.Value, value => ChallengeState.FromValue(value))
                        .HasColumnName("State")
                        .IsRequired();

                    challengeTestMode.Property(challenge => challenge.MatchQuantity)
                        .HasConversion(state => state.Value, value => TestModeMatchQuantity.FromValue(value))
                        .HasColumnName("AverageBestOf")
                        .IsRequired();

                    challengeTestMode.Property(challenge => challenge.ParticipantQuantity)
                        .HasConversion(state => state.Value, value => TestModeParticipantQuantity.FromValue(value))
                        .HasColumnName("ParticipantQuantity")
                        .IsRequired();
                }
            );

            builder.OwnsMany(
                challenge => challenge.Buckets,
                challengeStats =>
                {
                    challengeStats.ToTable("Payout");

                    challengeStats.HasForeignKey(nameof(ChallengeId));

                    challengeStats.Property<ChallengeId>(nameof(ChallengeId))
                        .HasConversion(entityId => entityId.ToGuid(), value => ChallengeId.FromGuid(value))
                        .HasColumnName(nameof(ChallengeId))
                        .IsRequired();

                    challengeStats.OwnsOne(bucket => bucket.Size).Property(size => size.Value).HasColumnName("Size").IsRequired();

                    challengeStats.OwnsOne(bucket => bucket.Prize).Property(prize => prize.Amount).HasColumnName("Prize").IsRequired();

                    challengeStats.OwnsOne(bucket => bucket.Prize).Enumeration(prize => prize.Type).HasColumnName("Currency").IsRequired();

                    challengeStats.Property<Guid>("Id").ValueGeneratedOnAdd().IsRequired();

                    challengeStats.HasKey(nameof(ChallengeId), "Id");
                }
            );

            builder.OwnsMany(
                challenge => challenge.Stats,
                challengeStats =>
                {
                    challengeStats.ToTable("Scoring");

                    challengeStats.HasForeignKey(nameof(ChallengeId));

                    challengeStats.Property<ChallengeId>(nameof(ChallengeId))
                        .HasConversion(entityId => entityId.ToGuid(), value => ChallengeId.FromGuid(value))
                        .HasColumnName(nameof(ChallengeId))
                        .IsRequired();

                    challengeStats.Property(stat => stat.Name)
                        .HasConversion(name => name.ToString(), name => new StatName(name))
                        .HasColumnName("Name")
                        .IsRequired();

                    challengeStats.Property(stat => stat.Weighting)
                        .HasConversion<float>(weighting => weighting, weighting => new StatWeighting(weighting))
                        .HasColumnName("Weighting")
                        .IsRequired();

                    challengeStats.Property<Guid>("Id").ValueGeneratedOnAdd().IsRequired();

                    challengeStats.HasKey(nameof(ChallengeId), "Id");
                }
            );

            builder.HasMany(challenge => challenge.Participants)
                .WithOne(participant => participant.Challenge)
                .HasForeignKey(nameof(ChallengeId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(Challenge.Participants)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(challenge => challenge.Id);
        }
    }
}
