// Filename: MatchConfiguration.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
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
            builder.ToTable("Match");

            builder.EntityId(match => match.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(match => match.Timestamp)
                .HasColumnName("Timestamp")
                .IsRequired();
            
            builder.Property(match => match.Reference)
                .HasConversion(externalId => externalId.ToString(), externalId => new MatchReference(externalId))
                .HasColumnName("Reference")
                .IsRequired();

            builder.Property<ParticipantId>(nameof(ParticipantId))
                .HasConversion(participantId => participantId.ToGuid(), participantId => ParticipantId.FromGuid(participantId))
                .HasColumnName(nameof(ParticipantId))
                .IsRequired();

            builder.Ignore(match => match.TotalScore);
            
            builder.OwnsMany(
                match => match.Stats,
                matchStats =>
                {
                    matchStats.ToTable("Stat");

                    matchStats.HasForeignKey(nameof(MatchId));

                    matchStats.Property<MatchId>(nameof(MatchId))
                        .HasConversion(entityId => entityId.ToGuid(), value => MatchId.FromGuid(value))
                        .HasColumnName(nameof(MatchId))
                        .IsRequired();

                    matchStats.Property(stat => stat.Name)
                        .HasConversion(name => name.ToString(), name => new StatName(name))
                        .HasColumnName("Name")
                        .IsRequired();

                    matchStats.Property(stat => stat.Value)
                        .HasConversion(statValue => statValue.Value, value => new StatValue(value))
                        .HasColumnName("Value")
                        .IsRequired();

                    matchStats.Property(stat => stat.Weighting)
                        .HasConversion(weighting => weighting.Value, weighting => new StatWeighting(weighting))
                        .HasColumnName("Weighting")
                        .IsRequired();

                    matchStats.Ignore(stat => stat.Score);

                    matchStats.Property<Guid>("Id").ValueGeneratedOnAdd().IsRequired();

                    matchStats.HasKey(nameof(MatchId), "Id");
                }
            );

            builder.HasKey(match => match.Id);
        }
    }
}
