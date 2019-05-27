// Filename: MatchConfiguration.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Seedwork.Infrastructure.Extensions;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Arena.Challenges.Infrastructure.Configurations
{
    internal sealed class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure([NotNull] EntityTypeBuilder<Match> builder)
        {
            builder.ToTable(nameof(ChallengesDbContext.Matches));

            builder.EntityId(match => match.Id)
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(match => match.Timestamp).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<ParticipantId>(nameof(ParticipantId))
                .HasConversion(participantId => participantId.ToGuid(), participantId => ParticipantId.FromGuid(participantId))
                .IsRequired();

            builder.Property(match => match.MatchExternalId)
                .HasConversion(externalId => externalId.ToString(), externalId => new MatchExternalId(externalId))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(match => match.TotalScore);

            builder.HasKey(match => match.Id);

            builder.HasMany(match => match.Stats).WithOne().HasForeignKey(stat => stat.MatchId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(Match.Stats)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
