// Filename: ChallengeModelConfiguration.cs
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
    internal sealed class ChallengeModelConfiguration : IEntityTypeConfiguration<ChallengeModel>
    {
        public void Configure(EntityTypeBuilder<ChallengeModel> builder)
        {
            builder.ToTable("Challenge");

            builder.Ignore(challenge => challenge.DomainEvents);

            builder.Property(challenge => challenge.Id).ValueGeneratedNever();

            builder.OwnsOne(challenge => challenge.Timeline);

            builder.OwnsMany(
                challenge => challenge.ScoringItems,
                scoringItem =>
                {
                    scoringItem.ToTable("ScoringItem");

                    scoringItem.WithOwner().HasForeignKey("ChallengeId");

                    scoringItem.Property<Guid>("Id").ValueGeneratedOnAdd();

                    scoringItem.HasKey("ChallengeId", "Id");
                });

            builder.HasMany(challenge => challenge.Participants).WithOne(participant => participant.Challenge).OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(challenge => challenge.Id);
        }
    }
}
