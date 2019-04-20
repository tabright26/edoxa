// Filename: ChallengeConfiguration.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.ValueObjects;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Newtonsoft.Json;

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

            builder.Property(challenge => challenge.Game).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(challenge => challenge.Name)
                .IsRequired()
                .HasConversion(name => name.ToString(), name => new ChallengeName(name))
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsOne(
                challenge => challenge.Settings,
                challengeSettings =>
                {
                    challengeSettings.Property(settings => settings.Type).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeSettings.Property(settings => settings.Entries)
                        .HasConversion<int>(entries => entries, entries => new Entries(entries, false))
                        .IsRequired()
                        .UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeSettings.Property(settings => settings.EntryFee)
                        .HasConversion<decimal>(entryFee => entryFee, entryFee => new EntryFee(entryFee, false))
                        .HasColumnType("decimal(4,2)")
                        .IsRequired()
                        .UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeSettings.Property(settings => settings.BestOf)
                        .HasConversion<int>(bestOf => bestOf, bestOf => new BestOf(bestOf, false))
                        .IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeSettings.Property(settings => settings.PayoutRatio)
                        .HasConversion<float>(payoutRatio => payoutRatio, payoutRatio => new PayoutRatio(payoutRatio, false)).IsRequired()
                        .UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeSettings.Property(settings => settings.ServiceChargeRatio)
                        .HasConversion<float>(serviceChargeRatio => serviceChargeRatio, serviceChargeRatio => new ServiceChargeRatio(serviceChargeRatio, false))
                        .IsRequired()
                        .UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeSettings.Property(settings => settings.Generated).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

                    challengeSettings.Ignore(settings => settings.PayoutEntries);

                    challengeSettings.Ignore(settings => settings.PrizePool);

                    challengeSettings.ToTable("ChallengeSettings");
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

                    challengeTimeline.ToTable("ChallengeTimelines");
                }
            );

            builder.Property(challenge => challenge.Scoring)
                .HasConversion(
                    scoring => JsonConvert.SerializeObject(scoring, Formatting.None),
                    scoring => JsonConvert.DeserializeObject<ChallengeScoring>(scoring)
                )
                .IsRequired(false)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(challenge => challenge.LiveData);

            builder.Ignore(challenge => challenge.Scoreboard);

            builder.Ignore(challenge => challenge.Payout);

            builder.HasMany(challenge => challenge.Participants)
                .WithOne(participant => participant.Challenge)
                .HasForeignKey(nameof(ChallengeId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(Challenge.Settings)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata.FindNavigation(nameof(Challenge.Timeline)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata.FindNavigation(nameof(Challenge.Participants)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(challenge => challenge.Id);
        }
    }
}