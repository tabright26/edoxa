// Filename: PayoutStrategy.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Data.Storage;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Strategies;
using eDoxa.Storage.Azure.File;

using IdentityServer4.Extensions;

namespace eDoxa.Cashier.Api.Areas.Challenges.Strategies
{
    public sealed class PayoutStrategy : IPayoutStrategy
    {
        public async Task<IPayout> GetPayoutAsync(PayoutEntries entries, EntryFee entryFee)
        {
            var storage = new CashierFileStorage(new AzureFileStorage());

            var payoutLookup = await storage.GetChallengePayoutStructuresAsync();

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
