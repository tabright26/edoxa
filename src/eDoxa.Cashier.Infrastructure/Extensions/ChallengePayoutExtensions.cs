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
                EntryFeeCurrency = model.Payout.EntryFee.Type.Value,
                EntryFeeAmount = model.Payout.EntryFee.Amount,
                Buckets = model.Payout.Buckets.Select(bucket => bucket.ToModel()).ToList()
            };
        }
    }
}
