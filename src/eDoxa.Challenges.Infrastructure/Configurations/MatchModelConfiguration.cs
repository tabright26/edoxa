// Filename: MatchModelConfiguration.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Challenges.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Challenges.Infrastructure.Configurations
{
    internal sealed class MatchModelConfiguration : IEntityTypeConfiguration<MatchModel>
    {
        public void Configure(EntityTypeBuilder<MatchModel> builder)
        {
            builder.ToTable("Match");

            builder.Ignore(match => match.DomainEvents);

            builder.Property(match => match.Id).ValueGeneratedNever();

            builder.OwnsMany(
                match => match.Stats,
                matchStats =>
                {
                    matchStats.ToTable("Stat");

                    matchStats.WithOwner().HasForeignKey("MatchId");

                    matchStats.Property<Guid>("Id").ValueGeneratedOnAdd();

                    matchStats.HasKey("MatchId", "Id");
                });

            builder.HasKey(match => match.Id);
        }
    }
}
