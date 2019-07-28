// Filename: ChallengeModelConfiguration.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Arena.Challenges.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Arena.Challenges.Infrastructure.Configurations
{
    internal sealed class ChallengeModelConfiguration : IEntityTypeConfiguration<ChallengeModel>
    {
        public void Configure( EntityTypeBuilder<ChallengeModel> builder)
        {
            builder.ToTable("Challenge");

            builder.Property(challenge => challenge.Id);

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

            //builder.OwnsMany(
            //    challenge => challenge.Buckets,
            //    challengeStats =>
            //    {
            //        challengeStats.ToTable("Bucket");

            //        challengeStats.HasForeignKey("ChallengeId");

            //        challengeStats.Property(bucket => bucket.PrizeAmount).HasColumnType("decimal(11, 2)");

            //        challengeStats.Property<Guid>("Id").ValueGeneratedOnAdd();

            //        challengeStats.HasKey("ChallengeId", "Id");
            //    }
            //);

            builder.HasMany(challenge => challenge.Participants).WithOne(participant => participant.Challenge).OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(challenge => challenge.Id);
        }
    }
}
