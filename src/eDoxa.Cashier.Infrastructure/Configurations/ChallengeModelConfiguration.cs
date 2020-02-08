// Filename: ChallengeModelConfiguration.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eDoxa.Cashier.Infrastructure.Configurations
{
    internal sealed class ChallengeModelConfiguration : IEntityTypeConfiguration<ChallengeModel>
    {
        public void Configure(EntityTypeBuilder<ChallengeModel> builder)
        {
            builder.ToTable("Challenge");

            builder.Ignore(challenge => challenge.DomainEvents);

            builder.Property(challenge => challenge.Id).IsRequired().ValueGeneratedNever();

            builder.Property(challenge => challenge.EntryFeeAmount).IsRequired().HasColumnType("decimal(11, 2)");

            builder.Property(challenge => challenge.EntryFeeCurrency).IsRequired();

            builder.OwnsMany(
                challenge => challenge.PayoutBuckets,
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

            builder.HasKey(challenge => challenge.Id);
        }
    }
}
