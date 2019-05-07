﻿// Filename: MatchConfiguration.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Entities.AggregateModels;
using eDoxa.Challenges.Domain.Entities.AggregateModels.MatchAggregate;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ParticipantAggregate;

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Challenges.Infrastructure.Configurations
{
    internal sealed class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure([NotNull] EntityTypeBuilder<Match> builder)
        {
            builder.ToTable(nameof(ChallengesDbContext.Matches));

            builder.Property(match => match.Id)
                   .HasConversion(matchId => matchId.ToGuid(), matchId => MatchId.FromGuid(matchId))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(match => match.Timestamp).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<ParticipantId>(nameof(ParticipantId))
                   .HasConversion(participantId => participantId.ToGuid(), participantId => ParticipantId.FromGuid(participantId))
                   .IsRequired();

            builder.Property(match => match.LinkedMatch)
                   .HasConversion(linkedMatch => linkedMatch.ToString(), linkedMatch => new LinkedMatch(linkedMatch))
                   .IsRequired()
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(match => match.TotalScore);

            builder.HasKey(match => match.Id);

            builder.HasMany(match => match.Stats).WithOne().HasForeignKey(stat => stat.MatchId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(Match.Stats)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}