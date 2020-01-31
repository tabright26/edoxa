// Filename: IChallengePayout.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public interface IChallengePayout
    {
        ChallengePayoutEntries Entries { get; }

        EntryFee EntryFee { get; }

        ChallengePayoutPrizePool PrizePool { get; }

        IChallengePayoutBuckets Buckets { get; }
    }
}
