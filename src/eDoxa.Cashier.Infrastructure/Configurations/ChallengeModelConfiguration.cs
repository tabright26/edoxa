// Filename: ChallengeModelConfiguration.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    internal sealed class ChallengeModelConfiguration : IEntityTypeConfiguration<ChallengeModel>
    {
        public void Configure( EntityTypeBuilder<ChallengeModel> builder)
        {
            builder.ToTable("Challenge");

            builder.Property(challenge => challenge.Id);

            builder.Property(challenge => challenge.EntryFeeAmount).HasColumnType("decimal(11, 2)");

            builder.OwnsMany(
                challenge => challenge.Buckets,
                challengeStats =>
                {
                    challengeStats.ToTable("Bucket");

                    challengeStats.WithOwner().HasForeignKey("ChallengeId");

                    challengeStats.Property(bucket => bucket.PrizeAmount).HasColumnType("decimal(11, 2)");

                    challengeStats.Property<Guid>("Id").ValueGeneratedOnAdd();

                    challengeStats.HasKey("ChallengeId", "Id");
                }
            );

            builder.HasKey(challenge => challenge.Id);
        }
    }
}
