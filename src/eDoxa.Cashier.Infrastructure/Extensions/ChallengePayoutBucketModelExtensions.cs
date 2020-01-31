// Filename: ChallengePayoutBucketModelExtensions.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Infrastructure.Models;

namespace eDoxa.Cashier.Infrastructure.Extensions
{
    public static class ChallengePayoutBucketModelExtensions
    {
        public static ChallengePayoutBucket ToEntity(this ChallengePayoutBucketModel model)
        {
            return new ChallengePayoutBucket(new ChallengePayoutBucketPrize(model.PrizeAmount, CurrencyType.FromValue(model.PrizeCurrency)), new ChallengePayoutBucketSize(model.Size));
        }
    }
}
