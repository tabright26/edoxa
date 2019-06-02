// Filename: ChallengeConfiguration.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;
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
            builder.ToTable(nameof(ChallengesDbContext.Challenges));

            builder.EntityId(challenge => challenge.Id).IsRequired();

            builder.Enumeration(challenge => challenge.Game).IsRequired();

            builder.OwnsOne(challenge => challenge.Name).Property(name => name.Value).IsRequired();

            builder.Property(challenge => challenge.CreatedAt).IsRequired();

            builder.OwnsOne(
                challenge => challenge.Timeline,
                challengeTimeline =>
                {
                    challengeTimeline.Property(challenge => challenge.Duration)
                        .HasConversion(duration => ((TimeSpan) duration).Ticks, duration => new ChallengeDuration(duration))
                        .IsRequired();

                    challengeTimeline.Property(challenge => challenge.StartedAt).IsRequired(false);

                    challengeTimeline.Property(challenge => challenge.ClosedAt).IsRequired(false);

                    challengeTimeline.Ignore(challenge => challenge.EndedAt);
                }
            );

            builder.OwnsOne(
                challenge => challenge.Setup,
                challengeSetup =>
                {
                    challengeSetup.OwnsOne(setup => setup.BestOf).Property(bestOf => bestOf.Value).IsRequired();

                    challengeSetup.OwnsOne(setup => setup.Entries).Property(entries => entries.Value).IsRequired();

                    challengeSetup.OwnsOne(setup => setup.PayoutRatio).Property(payoutRatio => payoutRatio.Value).IsRequired();

                    challengeSetup.OwnsOne(setup => setup.ServiceChargeRatio).Property(serviceChargeRatio => serviceChargeRatio.Value).IsRequired();

                    challengeSetup.OwnsOne(challenge => challenge.EntryFee).Property(entryFee => entryFee.Amount).IsRequired();

                    challengeSetup.OwnsOne(challenge => challenge.EntryFee)
                        .Property(entryFee => entryFee.Type)
                        .HasConversion(entityId => entityId.Value, entityId => Enumeration<CurrencyType>.FromValue(entityId))
                        .IsRequired();

                    challengeSetup.Ignore(setup => setup.PayoutEntries);

                    challengeSetup.Ignore(setup => setup.PrizePool);
                }
            );

            builder.OwnsMany(
                challenge => challenge.Stats,
                challengeStats =>
                {
                    challengeStats.ToTable(nameof(Scoring));

                    challengeStats.HasForeignKey(nameof(ChallengeId));

                    challengeStats.Property<ChallengeId>(nameof(ChallengeId))
                        .HasConversion(challengeId => challengeId.ToGuid(), value => ChallengeId.FromGuid(value))
                        .HasColumnName(nameof(ChallengeId))
                        .IsRequired();

                    challengeStats.Property<Guid>("Id").ValueGeneratedOnAdd().IsRequired();

                    challengeStats.OwnsOne(stat => stat.Name).Property(name => name.Value).HasColumnName(nameof(ChallengeStat.Name)).IsRequired();

                    challengeStats.OwnsOne(stat => stat.Weighting)
                        .Property(weighting => weighting.Value)
                        .HasColumnName(nameof(ChallengeStat.Weighting))
                        .IsRequired();

                    challengeStats.HasKey(nameof(ChallengeId), "Id");
                }
            );

            builder.OwnsMany(
                challenge => challenge.Buckets,
                challengeStats =>
                {
                    challengeStats.ToTable(nameof(Payout));

                    challengeStats.HasForeignKey(nameof(ChallengeId));

                    challengeStats.Property<Guid>("Id").ValueGeneratedOnAdd().IsRequired();

                    challengeStats.Property<ChallengeId>(nameof(ChallengeId))
                        .HasConversion(challengeId => challengeId.ToGuid(), value => ChallengeId.FromGuid(value))
                        .HasColumnName(nameof(ChallengeId))
                        .IsRequired();

                    challengeStats.Property(bucket => bucket.Size)
                        .HasConversion(name => name.Value, value => new BucketSize(value))
                        .HasColumnName(nameof(ChallengeStat.Name))
                        .IsRequired();

                    challengeStats.OwnsOne(bucket => bucket.Prize).Property(prize => prize.Amount).IsRequired();

                    challengeStats.OwnsOne(bucket => bucket.Prize)
                        .Property(prize => prize.Type)
                        .HasConversion(currencyType => currencyType.Value, value => CurrencyType.FromValue(value))
                        .IsRequired();

                    challengeStats.Ignore(bucket => bucket.Items);

                    challengeStats.HasKey(nameof(ChallengeId), "Id");
                }
            );

            builder.Property(challenge => challenge.TestMode).IsRequired();

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
