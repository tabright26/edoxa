// Filename: IPayout.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public interface IChallengePayout
    {
        ChallengePayoutEntries Entries { get; }

        PrizePool PrizePool { get; }

        IChallengePayoutBuckets Buckets { get; }
    }
}
