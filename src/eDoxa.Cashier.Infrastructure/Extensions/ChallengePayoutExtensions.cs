// Filename: ChallengePayoutExtensions.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Infrastructure.Models;

namespace eDoxa.Cashier.Infrastructure.Extensions
{
    public static class ChallengePayoutExtensions
    {
        public static ChallengePayoutModel ToModel(this IChallenge model)
        {
            return new ChallengePayoutModel
            {
                ChallengeId = model.Id,
                EntryFeeCurrency = model.EntryFee.Currency.Value,
                EntryFeeAmount = model.EntryFee.Amount,
                Buckets = model.Payout.Buckets.Select(bucket => bucket.ToModel()).ToList()
            };
        }
    }
}
