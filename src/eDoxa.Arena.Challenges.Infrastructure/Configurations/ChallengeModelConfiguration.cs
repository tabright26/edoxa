// Filename: ChallengeModelConfiguration.cs
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
    internal sealed class ChallengeModelConfiguration : IEntityTypeConfiguration<ChallengeModel>
    {
        public void Configure([NotNull] EntityTypeBuilder<ChallengeModel> builder)
        {
            builder.ToTable("Challenge");

            builder.Property(challenge => challenge.Id);

            builder.OwnsOne(challenge => challenge.Setup);

            builder.OwnsOne(challenge => challenge.Timeline);

            builder.OwnsMany(
                challenge => challenge.ScoringItems,
                challengeStats =>
                {
                    challengeStats.ToTable("ScoringItem");

                    challengeStats.HasForeignKey("ChallengeId");

                    challengeStats.Property<Guid>("Id").ValueGeneratedOnAdd();

                    challengeStats.HasKey("ChallengeId", "Id");
                }
            );

            builder.OwnsMany(
                challenge => challenge.Buckets,
                challengeStats =>
                {
                    challengeStats.ToTable("Bucket");

                    challengeStats.HasForeignKey("ChallengeId");

                    challengeStats.Property<Guid>("Id").ValueGeneratedOnAdd();

                    challengeStats.HasKey("ChallengeId", "Id");
                }
            );

            builder.HasMany(challenge => challenge.Participants).WithOne(participant => participant.Challenge);

            builder.HasKey(challenge => challenge.Id);
        }
    }
}
