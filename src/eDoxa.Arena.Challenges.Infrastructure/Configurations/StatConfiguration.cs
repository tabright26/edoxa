// Filename: StatConfiguration.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Arena.Challenges.Infrastructure.Configurations
{
    internal sealed class StatConfiguration : IEntityTypeConfiguration<Stat>
    {
        public void Configure([NotNull] EntityTypeBuilder<Stat> builder)
        {
            builder.ToTable(nameof(ChallengesDbContext.Stats));

            builder.Property(stat => stat.Id)
                .HasConversion(matchId => matchId.ToGuid(), value => StatId.FromGuid(value))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(stat => stat.MatchId)
                .HasConversion(matchId => matchId.ToGuid(), value => MatchId.FromGuid(value))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(stat => stat.Name)
                .HasConversion(name => name.ToString(), name => new StatName(name))
                .IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(stat => stat.Value)
                .HasConversion<double>(value => value, value => new StatValue(value)).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(stat => stat.Weighting)
                .HasConversion<float>(weighting => weighting, weighting => new StatWeighting(weighting)).IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(stat => stat.Score);

            builder.HasKey(stat => stat.Id);
        }
    }
}