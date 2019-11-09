// Filename: PayoutStrategy.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Cashier.Api.Infrastructure.Data.Storage;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Strategies;

using IdentityServer4.Extensions;

namespace eDoxa.Cashier.Api.Areas.Challenges.Strategies
{
    public sealed class ChallengePayoutStrategy : IChallengePayoutStrategy
    {
        public IPayout GetPayout(PayoutEntries entries, EntryFee entryFee)
        {
            var payoutLookup = FileStorage.ChallengePayouts;

            var payoutLevels = payoutLookup[entries].ToList();

            if (payoutLevels.IsNullOrEmpty())
            {
                throw new NotSupportedException($"Payout entries value ({entries}) is not supported.");
            }

            var prize = entryFee.GetLowestPrize();

            var buckets = new Buckets(
                payoutLevels.Select(payoutLevel => new Bucket(prize.ApplyWeighting(payoutLevel.Weighting), new BucketSize(payoutLevel.BucketSize))));

            return new Payout(buckets);
        }
    }
}
