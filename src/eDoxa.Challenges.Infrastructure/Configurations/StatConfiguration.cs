// Filename: StatConfiguration.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Challenges.Infrastructure.Configurations
{
    internal sealed class StatConfiguration : IEntityTypeConfiguration<Stat>
    {
        public void Configure(EntityTypeBuilder<Stat> builder)
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

            builder.Property(stat => stat.Name).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(stat => stat.Value).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(stat => stat.Weighting).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(stat => stat.Score);

            builder.HasKey(stat => stat.Id);
        }
    }
}