// Filename: MatchModelConfiguration.cs
// Date Created: 2019-06-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Arena.Challenges.Infrastructure.Configurations
{
    internal sealed class MatchModelConfiguration : IEntityTypeConfiguration<MatchModel>
    {
        public void Configure([NotNull] EntityTypeBuilder<MatchModel> builder)
        {
            builder.ToTable("Match");

            builder.Property(match => match.Id);

            builder.OwnsMany(
                match => match.Stats,
                matchStats =>
                {
                    matchStats.ToTable("Stat");

                    matchStats.HasForeignKey("MatchId");

                    matchStats.Property<Guid>("Id").ValueGeneratedOnAdd();

                    matchStats.HasKey("MatchId", "Id");
                }
            );

            builder.HasKey(match => match.Id);

            builder.HasIndex(match => match.GameMatchId).IsUnique();
        }
    }
}
