// Filename: ChallengePayoutModelExtensions.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Infrastructure.Extensions
{
    public static class ChallengePayoutModelExtensions
    {
        public static IChallenge ToEntity(this ChallengeModel model)
        {
            var entryFee = new EntryFee(model.EntryFeeAmount, CurrencyType.FromValue(model.EntryFeeCurrency));

            var payout = new ChallengePayout(entryFee, new ChallengePayoutBuckets(model.PayoutBuckets.Select(bucket => bucket.ToEntity())));

            var challenge = new Challenge(ChallengeId.FromGuid(model.Id), payout);

            challenge.ClearDomainEvents();

            return challenge;
        }
    }
}
