// Filename: DefaultChallengePayoutStrategy.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Cashier.Api.Infrastructure.Data.Storage;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Strategies;

using IdentityServer4.Extensions;

namespace eDoxa.Cashier.Api.Application.Strategies
{
    public sealed class DefaultChallengePayoutStrategy : IChallengePayoutStrategy
    {
        public IChallengePayout GetChallengePayout(ChallengePayoutEntries entries, EntryFee entryFee)
        {
            var charts = FileStorage.ChallengePayouts[entries].ToList();

            if (charts.IsNullOrEmpty())
            {
                throw new NotSupportedException($"Payout entries value ({entries}) is not supported.");
            }

            var prize = entryFee.GetPayoutBucketPrizeOrDefault();

            var buckets = new ChallengePayoutBuckets(charts.Select(chart => new ChallengePayoutBucket(prize.Apply(chart), chart.Size)));

            return new ChallengePayout(entryFee, buckets);
        }
    }
}
