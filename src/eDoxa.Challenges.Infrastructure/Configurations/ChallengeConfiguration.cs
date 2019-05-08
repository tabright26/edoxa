// Filename: ChallengeConfiguration.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.Entities;
using eDoxa.Challenges.Domain.Entities.AggregateModels;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Converters;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Enumerations;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Challenges.Infrastructure.Configurations
{
    public sealed class ChallengeConfiguration : IEntityTypeConfiguration<Challenge>
    {
        public void Configure([NotNull] EntityTypeBuilder<Challenge> builder)
        {
            builder.ToTable(nameof(ChallengesDbContext.Challenges));

            builder.Property(challenge => challenge.Id)
                .HasConversion(challengeId => challengeId.ToGuid(), value => ChallengeId.FromGuid(value))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(challenge => challenge.Game)
                .HasConversion(type => type.Value, value => Game.FromValue(value))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(challenge => challenge.Name)
                .HasConversion(name => name.ToString(), name => new ChallengeName(name))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsOne(
                challenge => challenge.Setup,
                challengeSetup =>
                {
                    challengeSetup.Property(setup => setup.Type)
                        .HasConversion(type => type.Value, value => ChallengeType.FromValue(value))
                        .IsRequired()
                        .HasColumnName(nameof(ChallengeSetup.Type))
                        .UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeSetup.Property(setup => setup.Entries)
                        .HasConversion<int>(entries => entries, entries => new Entries(entries, false))
                        .IsRequired()
                        .HasColumnName(nameof(ChallengeSetup.Entries))
                        .UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeSetup.Property(setup => setup.EntryFee)
                        .HasConversion<decimal>(entryFee => entryFee, entryFee => new EntryFee(entryFee, false))
                        .HasColumnType("decimal(4,2)")
                        .IsRequired()
                        .HasColumnName(nameof(ChallengeSetup.EntryFee))
                        .UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeSetup.Property(setup => setup.BestOf)
                        .HasConversion<int>(bestOf => bestOf, bestOf => new BestOf(bestOf, false))
                        .IsRequired()
                        .HasColumnName(nameof(ChallengeSetup.BestOf))
                        .UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeSetup.Property(setup => setup.PayoutRatio)
                        .HasConversion<float>(payoutRatio => payoutRatio, payoutRatio => new PayoutRatio(payoutRatio, false))
                        .IsRequired()
                        .HasColumnName(nameof(ChallengeSetup.PayoutRatio))
                        .UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeSetup.Property(setup => setup.ServiceChargeRatio)
                        .HasConversion<float>(serviceChargeRatio => serviceChargeRatio, serviceChargeRatio => new ServiceChargeRatio(serviceChargeRatio, false))
                        .IsRequired()
                        .HasColumnName(nameof(ChallengeSetup.ServiceChargeRatio))
                        .UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeSetup.Ignore(setup => setup.PayoutEntries);

                    challengeSetup.Ignore(setup => setup.PrizePool);
                }
            );

            builder.OwnsOne(
                challenge => challenge.Timeline,
                challengeTimeline =>
                {
                    challengeTimeline.Property(timeline => timeline.LiveMode).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeTimeline.Property(timeline => timeline.CreatedAt).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeTimeline.Property(timeline => timeline.PublishedAt).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeTimeline.Property(timeline => timeline.RegistrationPeriod)
                        .HasConversion(
                            timeSpan => timeSpan.HasValue ? (long?) timeSpan.Value.Ticks : null,
                            ticks => ticks.HasValue ? (TimeSpan?) TimeSpan.FromTicks(ticks.Value) : null
                        )
                        .IsRequired(false)
                        .UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeTimeline.Property(timeline => timeline.ExtensionPeriod)
                        .HasConversion(
                            timeSpan => timeSpan.HasValue ? (long?) timeSpan.Value.Ticks : null,
                            ticks => ticks.HasValue ? (TimeSpan?) TimeSpan.FromTicks(ticks.Value) : null
                        )
                        .IsRequired(false)
                        .UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeTimeline.Ignore(timeline => timeline.StartedAt);

                    challengeTimeline.Ignore(timeline => timeline.EndedAt);

                    challengeTimeline.Property(timeline => timeline.ClosedAt).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeTimeline.ToTable("Timelines");
                }
            );

            builder.Property(challenge => challenge.Scoring)
                .HasConversion(new ChallengeScoringConverter())
                .IsRequired(false)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(challenge => challenge.Scoreboard);

            builder.HasMany(challenge => challenge.Participants)
                .WithOne(participant => participant.Challenge)
                .HasForeignKey(nameof(ChallengeId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(Challenge.Setup)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata.FindNavigation(nameof(Challenge.Timeline)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata.FindNavigation(nameof(Challenge.Participants)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(challenge => challenge.Id);
        }
    }
}