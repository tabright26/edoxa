// Filename: ChallengeConfiguration.cs
// Date Created: 2019-05-20
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
using eDoxa.Arena.Challenges.Infrastructure.Converters;
using eDoxa.Arena.Challenges.Infrastructure.Extensions;
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

            builder.Property(challenge => challenge.Name).HasConversion(name => name.ToString(), name => new ChallengeName(name)).IsRequired();

            builder.Property(challenge => challenge.Duration)
                .HasConversion(duration => ((TimeSpan) duration).Ticks, duration => new ChallengeDuration(duration))
                .IsRequired();

            builder.Property(challenge => challenge.CreatedAt)
                .HasConversion<DateTime>(duration => duration, duration => new ChallengeCreatedAt(duration))
                .IsRequired();

            builder.Property(challenge => challenge.StartedAt)
                .HasConversion<DateTime?>(startedAt => startedAt, startedAt => startedAt.HasValue ? new ChallengeStartedAt(startedAt.Value) : null)
                .IsRequired(false);

            builder.Ignore(challenge => challenge.EndedAt);

            builder.Property(challenge => challenge.CompletedAt)
                .HasConversion<DateTime?>(startedAt => startedAt, startedAt => startedAt.HasValue ? new ChallengeCompletedAt(startedAt.Value) : null)
                .IsRequired(false);

            builder.Property(challenge => challenge.IsFake).IsRequired();

            builder.OwnsOne(
                challenge => challenge.Setup,
                challengeSetup =>
                {
                    challengeSetup.Property(setup => setup.BestOf)
                        .HasConversion<int>(bestOf => bestOf, bestOf => new BestOf(bestOf))
                        .IsRequired()
                        .HasColumnName(nameof(ChallengeSetup.BestOf));

                    challengeSetup.Property(setup => setup.Entries)
                        .HasConversion<int>(entries => entries, entries => new Entries(entries))
                        .IsRequired()
                        .HasColumnName(nameof(ChallengeSetup.Entries));

                    challengeSetup.Property(setup => setup.PayoutRatio)
                        .HasConversion<float>(payoutRatio => payoutRatio, payoutRatio => new PayoutRatio(payoutRatio))
                        .IsRequired()
                        .HasColumnName(nameof(ChallengeSetup.PayoutRatio));

                    challengeSetup.Property(setup => setup.ServiceChargeRatio)
                        .HasConversion<float>(serviceChargeRatio => serviceChargeRatio, serviceChargeRatio => new ServiceChargeRatio(serviceChargeRatio))
                        .IsRequired()
                        .HasColumnName(nameof(ChallengeSetup.ServiceChargeRatio));

                    challengeSetup.Property(setup => setup.EntryFee)
                        .HasConversion(entryFee => entryFee.Serialize(), entryFee => EntryFeeExtensions.Deserialize(entryFee))
                        .IsRequired()
                        .HasColumnName(nameof(ChallengeSetup.EntryFee));

                    challengeSetup.Ignore(setup => setup.PayoutEntries);

                    challengeSetup.Ignore(setup => setup.PrizePool);
                }
            );

            builder.Property(challenge => challenge.Scoring).HasConversion(new ScoringConverter()).IsRequired();

            builder.Ignore(challenge => challenge.Payout);

            builder.Ignore(challenge => challenge.Scoreboard);

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
