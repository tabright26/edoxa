// Filename: ChallengePayoutModelConfiguration.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    internal sealed class ChallengePayoutModelConfiguration : IEntityTypeConfiguration<ChallengePayoutModel>
    {
        public void Configure(EntityTypeBuilder<ChallengePayoutModel> builder)
        {
            builder.ToTable("ChallengePayout");

            builder.Ignore(challengePayout => challengePayout.DomainEvents);

            builder.Property(challengePayout => challengePayout.ChallengeId).HasColumnName("ChallengeId").IsRequired().ValueGeneratedNever();

            builder.Property(challengePayout => challengePayout.EntryFeeAmount).IsRequired().HasColumnType("decimal(11, 2)");

            builder.Property(challengePayout => challengePayout.EntryFeeCurrency).IsRequired();

            builder.OwnsMany(
                challengePayout => challengePayout.Buckets,
                challengePayoutBuckets =>
                {
                    challengePayoutBuckets.ToTable("ChallengePayoutBucket");

                    challengePayoutBuckets.WithOwner().HasForeignKey(challengePayoutBucket => challengePayoutBucket.ChallengeId);

                    challengePayoutBuckets.Property(challengePayoutBucket => challengePayoutBucket.Id).IsRequired().ValueGeneratedOnAdd();

                    challengePayoutBuckets.Property(challengePayoutBucket => challengePayoutBucket.ChallengeId).IsRequired();

                    challengePayoutBuckets.Property(challengePayoutBucket => challengePayoutBucket.PrizeAmount).IsRequired().HasColumnType("decimal(11, 2)");

                    challengePayoutBuckets.Property(challengePayoutBucket => challengePayoutBucket.PrizeCurrency).IsRequired();

                    challengePayoutBuckets.HasKey(
                        challengePayoutBucket => new
                        {
                            challengePayoutBucket.ChallengeId,
                            challengePayoutBucket.Id
                        });
                });

            builder.HasKey(challengePayout => challengePayout.ChallengeId);
        }
    }
}
